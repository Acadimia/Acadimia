using Acadimia.Data.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Bookings;
using Acadimia.Infrastructure.Services.Notifications;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Acadimia.Infrastructure.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        // Fallback if no PlatformCommissionSetting row is active - keeps this
        // in sync with the same commission concept used by FR-W07.
        private const decimal DefaultCommissionPercentage = 10m;

        // FR-S14: a paid (Confirmed) booking can be cancelled *by the student* only if the
        // session starts more than this many hours from now. Instructors are not limited by it.
        private const int StudentCancellationDeadlineHours = 24;

        private static readonly Expression<Func<BookingRescheduleRequest, RescheduleRequestDto>> RescheduleToDto = r => new RescheduleRequestDto
        {
            Id = r.Id,
            BookingId = r.BookingId,
            TeacherName = r.Booking.Teacher.User != null ? r.Booking.Teacher.User.Name : null,
            StudentName = r.Booking.Student != null ? r.Booking.Student.Name : null,
            OriginalDate = r.OriginalDate,
            OriginalStartTime = r.OriginalStartTime,
            ProposedDate = r.ProposedDate,
            ProposedStartTime = r.ProposedStartTime,
            Note = r.Note,
            Status = r.Status,
            RejectionReason = r.RejectionReason,
            CreatedOn = r.CreatedOn
        };

        public BookingService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        // Overlap check done in memory: TimeSpan arithmetic inside the LINQ query is not reliably translatable to SQL.
        private async Task<bool> HasBookingConflictAsync(int teacherId, DateTime date, TimeSpan start, int durationMinutes, int? excludeBookingId)
        {
            var dayStart = date.Date;
            var dayEnd = dayStart.AddDays(1);
            var end = start.Add(TimeSpan.FromMinutes(durationMinutes));

            var sameDay = await _context.Bookings
                .Where(b => b.TeacherId == teacherId
                            && b.Date >= dayStart && b.Date < dayEnd
                            && (excludeBookingId == null || b.Id != excludeBookingId)
                            && (b.Status == BookingStatus.Pending || b.Status == BookingStatus.Accepted || b.Status == BookingStatus.Confirmed))
                .Select(b => new { b.StartTime, b.DurationMinutes })
                .ToListAsync();

            return sameDay.Any(b => b.StartTime < end && start < b.StartTime.Add(TimeSpan.FromMinutes(b.DurationMinutes)));
        }

        public async Task<OperationResult> SubmitBookingRequestAsync(string studentId, BookingInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var teacher = await _context.Teachers.Include(t => t.User).SingleOrDefaultAsync(t => t.Id == input.TeacherId);
            if (teacher == null) { result.Message = Messages.Failed; return result; }

            var price = input.TeachingMode == CourseDeliveryType.Online ? teacher.HourlyPriceOnline : teacher.HourlyPriceInPerson;
            if (price == null) { result.Message = Messages.Failed; return result; } // teacher doesn't offer this mode

            var sessionFee = Math.Round(price.Value * input.DurationMinutes / 60m, 2);

            // FR-T05 exception flow: slot no longer available / conflicts.
            if (await HasBookingConflictAsync(input.TeacherId, input.Date, input.StartTime, input.DurationMinutes, null))
            { result.Message = "الموعد المطلوب لم يعد متاحاً"; return result; }

            var booking = new Booking
            {
                TeacherId = input.TeacherId,
                StudentId = studentId,
                SubjectId = input.SubjectId,
                GradeId = input.GradeId,
                TeachingMode = input.TeachingMode,
                Date = input.Date,
                StartTime = input.StartTime,
                DurationMinutes = input.DurationMinutes,
                Price = sessionFee,
                Status = BookingStatus.Pending,
                StudentNote = input.StudentNote,
                CreatedBy = studentId,
                CreatedOn = DateTime.Now
            };

            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();

            if (teacher.UserId != null)
                await _notificationService.CreateAsync(teacher.UserId, "طلب حجز جديد",
                    "لديك طلب حجز جلسة جديد بانتظار الرد.", NotificationType.Booking, nameof(Booking), booking.Id);

            result.Success = true;
            result.Message = Messages.Success;
            result.ReturnId = booking.Id;
            return result;
        }

        public async Task<OperationResult> DecideBookingAsync(string userId, BookingDecisionDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var booking = await _context.Bookings.Include(b => b.Teacher).ThenInclude(t => t.User)
                .SingleOrDefaultAsync(b => b.Id == input.BookingId);
            if (booking == null || booking.Status != BookingStatus.Pending) { result.Message = Messages.Failed; return result; }
            if (booking.Teacher.UserId != userId) { result.Message = Messages.Failed; return result; } // FR-T10 + ownership

            var studentUserId = booking.StudentId;

            if (!input.Accept)
            {
                booking.Status = BookingStatus.Rejected;
                booking.RejectionReason = input.RejectionReason;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();

                await _notificationService.CreateAsync(studentUserId, "تم رفض طلب الحجز",
                    "قام المعلم برفض طلب الحجز.", NotificationType.Booking, nameof(Booking), booking.Id);

                result.Success = true;
                result.Message = Messages.Success;
                return result;
            }

            // Accept path: deduct student, credit instructor net-of-commission,
            // one atomic transaction (FR-T06 / NFR-04) - same pattern as EnrollmentDeduction/InstructorCredit.
            //
            // Program.cs enables EnableRetryOnFailure, and a retrying execution strategy refuses
            // user-initiated transactions unless the whole unit of work runs inside strategy.ExecuteAsync.
            var strategy = _context.Database.CreateExecutionStrategy();
            OperationResult outcome;
            try
            {
                outcome = await strategy.ExecuteAsync(async () =>
                {
                    // A retry must start from a clean state, so reload everything inside the unit of work.
                    _context.ChangeTracker.Clear();
                    var attempt = new OperationResult(false, Messages.Invalid);

                    var current = await _context.Bookings.Include(b => b.Teacher).ThenInclude(t => t.User)
                        .SingleAsync(b => b.Id == input.BookingId);
                    if (current.Status != BookingStatus.Pending) { attempt.Message = Messages.Failed; return attempt; }

                    await using var transaction = await _context.Database.BeginTransactionAsync();

                    var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == current.StudentId);
                    if (wallet == null || wallet.Balance < current.Price)
                    {
                        attempt.Message = "رصيد المحفظة غير كافٍ"; // FR-T06 alt flow: booking stays unconfirmed
                        return attempt;
                    }

                    var commissionRate = await _context.PlatformCommissionSettings
                        .Where(s => s.IsActive).Select(s => (decimal?)s.CommissionPercentage).FirstOrDefaultAsync()
                        ?? DefaultCommissionPercentage;

                    var commission = Math.Round(current.Price * commissionRate / 100m, 2);
                    var instructorNet = current.Price - commission;

                    wallet.Balance -= current.Price;
                    _context.Wallets.Update(wallet);

                    await _context.WalletTransactions.AddAsync(new WalletTransaction
                    {
                        WalletId = wallet.Id,
                        Direction = WalletTransactionDirection.Out,
                        Type = WalletTransactionType.EnrollmentDeduction,
                        Amount = current.Price,
                        Status = WalletTransactionStatus.Completed,
                        Description = $"رسوم حجز جلسة مع {current.Teacher.User?.Name}",
                        RelatedEntityType = nameof(Booking),
                        RelatedEntityId = current.Id,
                        CreatedOn = DateTime.Now
                    });

                    var teacherWallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == current.Teacher.UserId);
                    if (teacherWallet != null)
                    {
                        teacherWallet.Balance += instructorNet;
                        _context.Wallets.Update(teacherWallet);

                        await _context.WalletTransactions.AddAsync(new WalletTransaction
                        {
                            WalletId = teacherWallet.Id,
                            Direction = WalletTransactionDirection.In,
                            Type = WalletTransactionType.InstructorCredit,
                            Amount = instructorNet,
                            Status = WalletTransactionStatus.Completed,
                            Description = "صافي أجر جلسة بعد العمولة",
                            RelatedEntityType = nameof(Booking),
                            RelatedEntityId = current.Id,
                            CreatedOn = DateTime.Now
                        });
                    }

                    current.Status = BookingStatus.Confirmed;
                    current.PaidOn = DateTime.Now;
                    _context.Bookings.Update(current);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    attempt.Success = true;
                    attempt.Message = Messages.Success;
                    return attempt;
                });
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
                return result;
            }

            if (outcome.Success)
                await _notificationService.CreateAsync(studentUserId, "تم قبول الحجز وتأكيده",
                    "وافق المعلم على طلبك وتم خصم الرسوم من محفظتك.", NotificationType.Booking, nameof(Booking), input.BookingId);

            return outcome;
        }

        // FR-S14 (student) + instructor cancellation.
        //  - Pending / Accepted: nothing was charged, so just cancel.
        //  - Confirmed (paid): full refund to the student, and the instructor's net credit is taken back,
        //    in one atomic transaction. The student may not cancel within StudentCancellationDeadlineHours
        //    of the session start; the instructor may.
        public async Task<OperationResult> CancelBookingAsync(string userId, int bookingId, string? reason = null)
        {
            var result = new OperationResult(false, Messages.Invalid);
            string? notifyUserId = null;

            var strategy = _context.Database.CreateExecutionStrategy();
            OperationResult outcome;
            try
            {
                outcome = await strategy.ExecuteAsync(async () =>
                {
                    _context.ChangeTracker.Clear();
                    var attempt = new OperationResult(false, Messages.Invalid);

                    var booking = await _context.Bookings.Include(b => b.Teacher).SingleOrDefaultAsync(b => b.Id == bookingId);
                    if (booking == null) return attempt;

                    var isStudent = booking.StudentId == userId;
                    var isOwningTeacher = booking.Teacher.UserId == userId;
                    if (!isStudent && !isOwningTeacher) { attempt.Message = Messages.Failed; return attempt; }

                    if (booking.Status is BookingStatus.Completed or BookingStatus.Cancelled or BookingStatus.Rejected)
                    { attempt.Message = Messages.Failed; return attempt; }

                    var now = DateTime.Now;
                    var sessionStart = booking.Date.Date + booking.StartTime;
                    if (sessionStart <= now) { attempt.Message = "لا يمكن إلغاء حجز بدأ موعده"; return attempt; }

                    var isPaid = booking.Status == BookingStatus.Confirmed && booking.PaidOn != null;
                    if (isStudent && isPaid && sessionStart - now < TimeSpan.FromHours(StudentCancellationDeadlineHours))
                    {
                        attempt.Message = $"لا يمكن إلغاء الحجز المؤكد قبل أقل من {StudentCancellationDeadlineHours} ساعة من موعده";
                        return attempt;
                    }

                    await using var transaction = await _context.Database.BeginTransactionAsync();

                    if (isPaid)
                    {
                        var refundError = await ReverseBookingPaymentAsync(booking);
                        if (refundError != null) { attempt.Message = refundError; return attempt; }
                    }

                    // A pending reschedule request is meaningless once the booking is gone.
                    var pendingReschedules = await _context.BookingRescheduleRequests
                        .Where(r => r.BookingId == booking.Id && r.Status == RescheduleRequestStatus.Pending)
                        .ToListAsync();
                    foreach (var pending in pendingReschedules)
                    {
                        pending.Status = RescheduleRequestStatus.Cancelled;
                        pending.UpdatedBy = userId;
                        pending.UpdatedOn = now;
                    }

                    booking.Status = BookingStatus.Cancelled;
                    booking.CancellationReason = reason;
                    booking.CancelledOn = now;
                    booking.UpdatedBy = userId;
                    booking.UpdatedOn = now;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    notifyUserId = isStudent ? booking.Teacher.UserId : booking.StudentId;
                    attempt.Success = true;
                    attempt.Message = Messages.Success;
                    return attempt;
                });
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
                return result;
            }

            if (outcome.Success && notifyUserId != null)
                await _notificationService.CreateAsync(notifyUserId, "تم إلغاء الحجز", "تم إلغاء حجز الجلسة.",
                    NotificationType.Booking, nameof(Booking), bookingId);

            return outcome;
        }

        // Must be called inside the caller's transaction. Returns an error message, or null on success.
        private async Task<string?> ReverseBookingPaymentAsync(Booking booking)
        {
            var entries = await _context.WalletTransactions
                .Include(t => t.Wallet)
                .Where(t => t.RelatedEntityType == nameof(Booking)
                            && t.RelatedEntityId == booking.Id
                            && t.Status == WalletTransactionStatus.Completed
                            && (t.Type == WalletTransactionType.EnrollmentDeduction || t.Type == WalletTransactionType.InstructorCredit))
                .ToListAsync();

            var deduction = entries.FirstOrDefault(t => t.Type == WalletTransactionType.EnrollmentDeduction);
            var credit = entries.FirstOrDefault(t => t.Type == WalletTransactionType.InstructorCredit);
            if (deduction == null) return Messages.Failed; // paid booking without its payment record - do not guess

            var teacherWallet = credit?.Wallet;
            if (credit != null && teacherWallet != null && teacherWallet.Balance < credit.Amount)
                return "لا يمكن استرجاع المبلغ لأن رصيد المعلم لا يكفي، يرجى التواصل مع الإدارة";

            var studentWallet = deduction.Wallet;
            studentWallet.Balance += deduction.Amount;
            await _context.WalletTransactions.AddAsync(new WalletTransaction
            {
                WalletId = studentWallet.Id,
                Direction = WalletTransactionDirection.In,
                Type = WalletTransactionType.BookingRefund,
                Amount = deduction.Amount,
                Status = WalletTransactionStatus.Completed,
                Description = "استرجاع رسوم حجز ملغي",
                RelatedEntityType = nameof(Booking),
                RelatedEntityId = booking.Id,
                CreatedOn = DateTime.Now
            });

            if (credit != null && teacherWallet != null)
            {
                teacherWallet.Balance -= credit.Amount;
                await _context.WalletTransactions.AddAsync(new WalletTransaction
                {
                    WalletId = teacherWallet.Id,
                    Direction = WalletTransactionDirection.Out,
                    Type = WalletTransactionType.BookingRefundReversal,
                    Amount = credit.Amount,
                    Status = WalletTransactionStatus.Completed,
                    Description = "إلغاء أجر جلسة بعد إلغاء الحجز",
                    RelatedEntityType = nameof(Booking),
                    RelatedEntityId = booking.Id,
                    CreatedOn = DateTime.Now
                });
            }

            return null;
        }

        public async Task<OperationResult> CompleteBookingAsync(string userId, int bookingId)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var booking = await _context.Bookings.Include(b => b.Teacher).SingleOrDefaultAsync(b => b.Id == bookingId);
            if (booking == null || booking.Teacher.UserId != userId || booking.Status != BookingStatus.Confirmed)
            { result.Message = Messages.Failed; return result; }

            booking.Status = BookingStatus.Completed;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<OperationResult> RateTeacherAsync(string studentId, RateTeacherDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var booking = await _context.Bookings.SingleOrDefaultAsync(b => b.Id == input.BookingId);
            if (booking == null || booking.StudentId != studentId || booking.Status != BookingStatus.Completed)
            { result.Message = Messages.Failed; return result; } // FR-T11 exception flow

            var alreadyRated = await _context.Set<TeacherRating>().AnyAsync(r => r.BookingId == booking.Id);
            if (alreadyRated) { result.Message = Messages.Failed; return result; }

            await _context.Set<TeacherRating>().AddAsync(new TeacherRating
            {
                BookingId = booking.Id,
                TeacherId = booking.TeacherId,
                StudentId = studentId,
                RatingValue = input.RatingValue,
                Review = input.Review,
                CreatedBy = studentId,
                CreatedOn = DateTime.Now
            });
            await _context.SaveChangesAsync();

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<List<BookingDto>> GetMyBookingsAsync(string userId) =>
            await _context.Bookings.Where(b => b.StudentId == userId)
                .OrderByDescending(b => b.Date)
                .Select(BookingProjections.ToDto)
                .ToListAsync();

        public async Task<List<BookingDto>> GetTeacherBookingsAsync(string userId, BookingStatus? status)
        {
            var query = _context.Bookings.Where(b => b.Teacher.UserId == userId);
            if (status.HasValue)
            {
                var wanted = status.Value;
                query = query.Where(b => b.Status == wanted);
            }
            return await query.OrderByDescending(b => b.Date)
                .Select(BookingProjections.ToDto)
                .ToListAsync();
        }

        // ==================== FR-S15: reschedule requests ====================

        public async Task<OperationResult> RequestRescheduleAsync(string studentId, RescheduleRequestInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var booking = await _context.Bookings.Include(b => b.Teacher)
                .SingleOrDefaultAsync(b => b.Id == input.BookingId && b.StudentId == studentId);
            // Only a confirmed session can be moved; pending ones can simply be cancelled and re-booked.
            if (booking == null || booking.Status != BookingStatus.Confirmed) { result.Message = Messages.Failed; return result; }

            var now = DateTime.Now;
            var currentStart = booking.Date.Date + booking.StartTime;
            if (currentStart <= now) { result.Message = "لا يمكن طلب إعادة جدولة حجز بدأ موعده"; return result; }

            var proposedStart = input.ProposedDate.Date + input.ProposedStartTime;
            if (proposedStart <= now || proposedStart == currentStart) { result.Message = "الموعد المقترح غير صالح"; return result; }

            var hasPending = await _context.BookingRescheduleRequests
                .AnyAsync(r => r.BookingId == booking.Id && r.Status == RescheduleRequestStatus.Pending);
            if (hasPending) { result.Message = "يوجد طلب إعادة جدولة قيد المراجعة لهذا الحجز"; return result; }

            if (await HasBookingConflictAsync(booking.TeacherId, input.ProposedDate, input.ProposedStartTime, booking.DurationMinutes, booking.Id))
            { result.Message = "الموعد المقترح غير متاح، يرجى اختيار موعد آخر"; return result; }

            var request = new BookingRescheduleRequest
            {
                BookingId = booking.Id,
                OriginalDate = booking.Date,
                OriginalStartTime = booking.StartTime,
                ProposedDate = input.ProposedDate.Date,
                ProposedStartTime = input.ProposedStartTime,
                Note = input.Note,
                Status = RescheduleRequestStatus.Pending,
                CreatedBy = studentId,
                CreatedOn = now
            };
            await _context.BookingRescheduleRequests.AddAsync(request);
            await _context.SaveChangesAsync();

            if (booking.Teacher.UserId != null)
                await _notificationService.CreateAsync(booking.Teacher.UserId, "طلب إعادة جدولة",
                    "طلب الطالب تغيير موعد جلسة مؤكدة.", NotificationType.Booking, nameof(BookingRescheduleRequest), request.Id);

            result.Success = true;
            result.Message = Messages.Success;
            result.ReturnId = request.Id;
            return result;
        }

        public async Task<OperationResult> DecideRescheduleAsync(string teacherUserId, RescheduleDecisionDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var request = await _context.BookingRescheduleRequests
                .Include(r => r.Booking).ThenInclude(b => b.Teacher)
                .SingleOrDefaultAsync(r => r.Id == input.RequestId);

            if (request == null || request.Status != RescheduleRequestStatus.Pending
                || request.Booking.Teacher.UserId != teacherUserId
                || request.Booking.Status != BookingStatus.Confirmed)
            { result.Message = Messages.Failed; return result; }

            var booking = request.Booking;

            if (input.Approve)
            {
                if (await HasBookingConflictAsync(booking.TeacherId, request.ProposedDate, request.ProposedStartTime, booking.DurationMinutes, booking.Id))
                { result.Message = "الموعد المقترح لم يعد متاحاً"; return result; }

                booking.Date = request.ProposedDate;
                booking.StartTime = request.ProposedStartTime;
                booking.UpdatedBy = teacherUserId;
                booking.UpdatedOn = DateTime.Now;
                request.Status = RescheduleRequestStatus.Approved;
            }
            else
            {
                // The original confirmed schedule stays untouched.
                request.Status = RescheduleRequestStatus.Rejected;
                request.RejectionReason = input.RejectionReason;
            }

            request.DecisionBy = teacherUserId;
            request.DecisionOn = DateTime.Now;
            await _context.SaveChangesAsync();

            await _notificationService.CreateAsync(booking.StudentId,
                input.Approve ? "تمت الموافقة على إعادة الجدولة" : "تم رفض طلب إعادة الجدولة",
                input.Approve ? "تم تغيير موعد جلستك إلى الموعد الذي اقترحته." : "بقي موعد جلستك الأصلي كما هو.",
                NotificationType.Booking, nameof(Booking), booking.Id);

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        public async Task<List<RescheduleRequestDto>> GetMyRescheduleRequestsAsync(string studentId) =>
            await _context.BookingRescheduleRequests
                .Where(r => r.Booking.StudentId == studentId)
                .OrderByDescending(r => r.CreatedOn)
                .Select(RescheduleToDto)
                .ToListAsync();

        public async Task<List<RescheduleRequestDto>> GetTeacherRescheduleRequestsAsync(string teacherUserId, RescheduleRequestStatus? status)
        {
            var query = _context.BookingRescheduleRequests.Where(r => r.Booking.Teacher.UserId == teacherUserId);
            if (status.HasValue)
            {
                var wanted = status.Value;
                query = query.Where(r => r.Status == wanted);
            }
            return await query.OrderByDescending(r => r.CreatedOn).Select(RescheduleToDto).ToListAsync();
        }
    }
}
