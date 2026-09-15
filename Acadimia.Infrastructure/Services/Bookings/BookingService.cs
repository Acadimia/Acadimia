using Acadimia.Core.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Bookings;
using Acadimia.Infrastructure.Services.Notifications;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Infrastructure.Services.Bookings
{
    public class BookingService : IBookingService
    {
        private readonly ApplicationDbContext _context;
        private readonly INotificationService _notificationService;

        // Fallback if no PlatformCommissionSetting row is active - keeps this
        // in sync with the same commission concept used by FR-W07.
        private const decimal DefaultCommissionPercentage = 10m;

        public BookingService(ApplicationDbContext context, INotificationService notificationService)
        {
            _context = context;
            _notificationService = notificationService;
        }

        public async Task<OperationResult> SubmitBookingRequestAsync(string studentId, BookingInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var teacher = await _context.Teachers.Include(t => t.User).SingleOrDefaultAsync(t => t.Id == input.TeacherId);
            if (teacher == null) { result.Message = Messages.Failed; return result; }

            var price = input.TeachingMode == CourseDeliveryType.Online ? teacher.HourlyPriceOnline : teacher.HourlyPriceInPerson;
            if (price == null) { result.Message = Messages.Failed; return result; } // teacher doesn't offer this mode

            var sessionFee = Math.Round(price.Value * input.DurationMinutes / 60m, 2);
            var requestedEnd = input.StartTime.Add(TimeSpan.FromMinutes(input.DurationMinutes));

            // FR-T05 exception flow: slot no longer available / conflicts.
            var hasConflict = await _context.Bookings.AnyAsync(b =>
                b.TeacherId == input.TeacherId &&
                b.Date.Date == input.Date.Date &&
                (b.Status == BookingStatus.Accepted || b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.Pending) &&
                b.StartTime < requestedEnd &&
                input.StartTime < b.StartTime.Add(TimeSpan.FromMinutes(b.DurationMinutes)));

            if (hasConflict) { result.Message = "الموعد المطلوب لم يعد متاحاً"; return result; }

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

            if (!input.Accept)
            {
                booking.Status = BookingStatus.Rejected;
                booking.RejectionReason = input.RejectionReason;
                _context.Bookings.Update(booking);
                await _context.SaveChangesAsync();

                await _notificationService.CreateAsync(booking.StudentId, "تم رفض طلب الحجز",
                    "قام المعلم برفض طلب الحجز.", NotificationType.Booking, nameof(Booking), booking.Id);

                result.Success = true;
                result.Message = Messages.Success;
                return result;
            }

            // Accept path: deduct student, credit instructor net-of-commission,
            // one atomic transaction (FR-T06 / NFR-04) - same pattern as EnrollmentDeduction/InstructorCredit.
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var wallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == booking.StudentId);
                if (wallet == null || wallet.Balance < booking.Price)
                {
                    result.Message = "رصيد المحفظة غير كافٍ"; // FR-T06 alt flow: booking stays unconfirmed
                    return result;
                }

                var commissionRate = await _context.PlatformCommissionSettings
                    .Where(s => s.IsActive).Select(s => (decimal?)s.CommissionPercentage).FirstOrDefaultAsync()
                    ?? DefaultCommissionPercentage;

                var commission = Math.Round(booking.Price * commissionRate / 100m, 2);
                var instructorNet = booking.Price - commission;

                wallet.Balance -= booking.Price;
                _context.Wallets.Update(wallet);

                await _context.WalletTransactions.AddAsync(new WalletTransaction
                {
                    WalletId = wallet.Id,
                    Direction = WalletTransactionDirection.Out,
                    Type = WalletTransactionType.EnrollmentDeduction,
                    Amount = booking.Price,
                    Status = WalletTransactionStatus.Completed,
                    Description = $"رسوم حجز جلسة مع {booking.Teacher.User?.Name}",
                    RelatedEntityType = nameof(Booking),
                    RelatedEntityId = booking.Id,
                    CreatedOn = DateTime.Now
                });

                var teacherWallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == booking.Teacher.UserId);
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
                        RelatedEntityId = booking.Id,
                        CreatedOn = DateTime.Now
                    });
                }

                booking.Status = BookingStatus.Confirmed;
                booking.PaidOn = DateTime.Now;
                _context.Bookings.Update(booking);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                await _notificationService.CreateAsync(booking.StudentId, "تم قبول الحجز وتأكيده",
                    "وافق المعلم على طلبك وتم خصم الرسوم من محفظتك.", NotificationType.Booking, nameof(Booking), booking.Id);

                result.Success = true;
                result.Message = Messages.Success;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                result.Message = Messages.Failed;
            }

            return result;
        }

        public async Task<OperationResult> CancelBookingAsync(string userId, int bookingId)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var booking = await _context.Bookings.Include(b => b.Teacher).SingleOrDefaultAsync(b => b.Id == bookingId);
            if (booking == null) return result;

            var isStudent = booking.StudentId == userId;
            var isOwningTeacher = booking.Teacher.UserId == userId;
            if (!isStudent && !isOwningTeacher) { result.Message = Messages.Failed; return result; }
            if (booking.Status is BookingStatus.Completed or BookingStatus.Cancelled) { result.Message = Messages.Failed; return result; }

            booking.Status = BookingStatus.Cancelled;
            _context.Bookings.Update(booking);
            await _context.SaveChangesAsync();

            var notifyUserId = isStudent ? booking.Teacher.UserId : booking.StudentId;
            if (notifyUserId != null)
                await _notificationService.CreateAsync(notifyUserId, "تم إلغاء الحجز", "تم إلغاء حجز الجلسة.",
                    NotificationType.Booking, nameof(Booking), booking.Id);

            result.Success = true;
            result.Message = Messages.Success;
            return result;
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
            await _context.Bookings.Where(b => b.StudentId == userId).OrderByDescending(b => b.Date)
                .Select(b => ToDto(b)).ToListAsync();

        public async Task<List<BookingDto>> GetTeacherBookingsAsync(string userId, BookingStatus? status)
        {
            var query = _context.Bookings.Where(b => b.Teacher.UserId == userId);
            if (status.HasValue) query = query.Where(b => b.Status == status);
            return await query.OrderByDescending(b => b.Date).Select(b => ToDto(b)).ToListAsync();
        }

        private static BookingDto ToDto(Booking b) => new()
        {
            Id = b.Id,
            TeacherId = b.TeacherId,
            TeacherName = b.Teacher.User != null ? b.Teacher.User.Name : null,
            StudentId = b.StudentId,
            StudentName = b.Student != null ? b.Student.Name : null,
            SubjectId = b.SubjectId,
            SubjectName = b.Subject != null ? b.Subject.Name : null,
            TeachingMode = b.TeachingMode,
            Date = b.Date,
            StartTime = b.StartTime,
            DurationMinutes = b.DurationMinutes,
            Price = b.Price,
            Status = b.Status,
            StudentNote = b.StudentNote,
            RejectionReason = b.RejectionReason
        };
    }
}