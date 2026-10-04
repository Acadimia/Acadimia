using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.JoinRequests;
using Acadimia.Infrastructure.Services.Notifications;
using Acadimia.Infrastructure.Services.Ownership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Acadimia.Infrastructure.Services.JoinRequests
{
    public class JoinRequestService : BaseService, IJoinRequestService
    {
        // نفس القيمة الاحتياطية المستخدمة في BookingService
        private const decimal DefaultCommissionPercentage = 10m;

        private readonly INotificationService _notifications;
        private readonly IOwnershipService _ownership;

        public JoinRequestService(ApplicationDbContext context, UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor, INotificationService notifications, IOwnershipService ownership)
            : base(context, userManager, httpContextAccessor)
        {
            _notifications = notifications;
            _ownership = ownership;
        }

        private static readonly Expression<Func<JoinRequest, JoinRequestDto>> ToDto = j => new JoinRequestDto
        {
            Id = j.Id,
            StudentId = j.StudentId,
            StudentName = j.Student.Name,
            TargetType = j.TargetType,
            GroupId = j.GroupId,
            CourseId = j.CourseId,
            Title = j.Course != null ? j.Course.Title
                  : (j.Group != null ? j.Group.Name : null),
            Fee = j.Course != null ? j.Course.Price
                : (j.Group != null && j.Group.Course != null ? j.Group.Course.Price : 0m),
            Status = j.Status,
            RejectionReason = j.RejectionReason,
            CreatedOn = j.CreatedOn
        };

        // ==================== الطالب: تقديم طلب انضمام ====================

        public async Task<OperationResult> SubmitAsync(string userId, JoinRequestInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId == null) { result.Message = Messages.Failed; return result; }

            // لازم يكون الهدف واحد فقط ومطابق للنوع
            var isCourse = input.TargetType == JoinRequestTargetType.Course;
            if (isCourse && (input.CourseId == null || input.GroupId != null)) return result;
            if (!isCourse && (input.GroupId == null || input.CourseId != null)) return result;

            int teacherId;
            if (isCourse)
            {
                var course = await _context.Courses
                    .Where(c => c.Id == input.CourseId && c.Status == CourseStatus.Published)
                    .Select(c => new { c.TeacherId }).FirstOrDefaultAsync();
                if (course == null) { result.Message = Messages.Failed; return result; }
                teacherId = course.TeacherId;
            }
            else
            {
                var group = await _context.Groups.Where(g => g.Id == input.GroupId)
                    .Select(g => new { g.TeacherId }).FirstOrDefaultAsync();
                if (group == null) { result.Message = Messages.Failed; return result; }
                teacherId = group.TeacherId;
            }

            var sid = studentId.Value;
            var duplicatePending = await _context.JoinRequests.AnyAsync(j =>
                j.StudentId == sid && j.Status == JoinRequestStatus.Pending
                && j.GroupId == input.GroupId && j.CourseId == input.CourseId);
            var alreadyEnrolled = await _context.Enrollments.AnyAsync(e =>
                e.StudentId == sid && e.Status == EnrollmentStatus.Active
                && e.GroupId == input.GroupId && e.CourseId == input.CourseId);
            if (duplicatePending || alreadyEnrolled)
            {
                result.Message = "لديك طلب انضمام قائم أو أنت مسجّل بالفعل";
                return result;
            }

            var request = new JoinRequest
            {
                StudentId = sid,
                TargetType = input.TargetType,
                GroupId = input.GroupId,
                CourseId = input.CourseId,
                Status = JoinRequestStatus.Pending
            };
            SetCreatedFields(request, userId);
            await _context.JoinRequests.AddAsync(request);
            await _context.SaveChangesAsync();

            var teacherUserId = await _context.Teachers.Where(t => t.Id == teacherId)
                .Select(t => t.UserId).FirstOrDefaultAsync();
            if (teacherUserId != null)
                await _notifications.CreateAsync(teacherUserId, "طلب انضمام جديد",
                    "لديك طلب انضمام جديد بانتظار القرار.", NotificationType.JoinRequest,
                    nameof(JoinRequest), request.Id);

            result.Success = true;
            result.Message = Messages.Success;
            result.ReturnId = request.Id;
            return result;
        }

        public async Task<List<JoinRequestDto>> GetMyRequestsAsync(string userId)
        {
            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId == null) return new List<JoinRequestDto>();

            return await _context.JoinRequests.Where(j => j.StudentId == studentId)
                .OrderByDescending(j => j.CreatedOn).Select(ToDto).ToListAsync();
        }

        public async Task<List<JoinRequestDto>> GetPendingAsync(string userId)
        {
            var isAdmin = await _ownership.GetUserTypeIdAsync(userId) == UserTypeIds.Admin;

            var query = _context.JoinRequests.Where(j => j.Status == JoinRequestStatus.Pending);
            if (!isAdmin)
                query = query.Where(j =>
                    (j.Course != null && j.Course.Teacher.UserId == userId) ||
                    (j.Group != null && j.Group.Teacher.UserId == userId));

            return await query.OrderBy(j => j.CreatedOn).Select(ToDto).ToListAsync();
        }

        // ==================== المعلم/الأدمن: قبول أو رفض ====================

        public async Task<OperationResult> DecideAsync(string userId, JoinRequestDecisionDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);

            // صلاحية: الأدمن أو المعلم صاحب الكورس/المجموعة
            var header = await _context.JoinRequests.Where(j => j.Id == input.RequestId)
                .Select(j => new { j.Status, j.CourseId, j.GroupId, j.Student.UserId }).FirstOrDefaultAsync();
            if (header == null || header.Status != JoinRequestStatus.Pending)
            { result.Message = Messages.Failed; return result; }

            var isAdmin = await _ownership.GetUserTypeIdAsync(userId) == UserTypeIds.Admin;
            var owns = header.CourseId != null
                ? await _ownership.OwnsCourseAsync(userId, header.CourseId.Value)
                : header.GroupId != null && await _ownership.OwnsGroupAsync(userId, header.GroupId.Value);
            if (!isAdmin && !owns) { result.Message = Messages.Failed; return result; }

            var studentUserId = header.UserId;

            // ---------- رفض ----------
            if (!input.Approve)
            {
                var request = await _context.JoinRequests.SingleAsync(j => j.Id == input.RequestId);
                request.Status = JoinRequestStatus.Rejected;
                request.RejectionReason = input.RejectionReason;
                request.DecisionBy = userId;
                request.DecisionOn = DateTime.Now;
                SetUpdatedFields(request, userId);
                await _context.SaveChangesAsync();

                if (studentUserId != null)
                    await _notifications.CreateAsync(studentUserId, "تم رفض طلب الانضمام",
                        input.RejectionReason ?? "تم رفض طلب انضمامك.", NotificationType.JoinRequest,
                        nameof(JoinRequest), request.Id);

                result.Success = true;
                result.Message = Messages.Success;
                return result;
            }

            // ---------- قبول: خصم + تحويل + عمولة + تسجيل (Transaction واحدة) ----------
            var strategy = _context.Database.CreateExecutionStrategy();
            OperationResult outcome;
            try
            {
                outcome = await strategy.ExecuteAsync(async () =>
                {
                    _context.ChangeTracker.Clear();
                    var attempt = new OperationResult(false, Messages.Invalid);

                    var current = await _context.JoinRequests
                        .Include(j => j.Student)
                        .Include(j => j.Course)
                        .Include(j => j.Group).ThenInclude(g => g!.Course)
                        .SingleAsync(j => j.Id == input.RequestId);
                    if (current.Status != JoinRequestStatus.Pending) { attempt.Message = Messages.Failed; return attempt; }

                    var course = current.Course ?? current.Group?.Course;
                    var teacherId = current.Course != null ? current.Course.TeacherId : current.Group!.TeacherId;
                    var capacity = current.Course != null ? current.Course.MaxStudents : current.Group!.MaxStudents;
                    var fee = course?.Price ?? 0m; // مجموعة بدون كورس = بدون رسوم

                    // السعة القصوى
                    if (capacity > 0)
                    {
                        var activeCount = await _context.Enrollments.CountAsync(e =>
                            e.Status == EnrollmentStatus.Active
                            && e.GroupId == current.GroupId && e.CourseId == current.CourseId);
                        if (activeCount >= capacity) { attempt.Message = "اكتمل العدد الأقصى للطلاب"; return attempt; }
                    }

                    var studentUser = current.Student.UserId;
                    var teacherUser = await _context.Teachers.Where(t => t.Id == teacherId)
                        .Select(t => t.UserId).FirstOrDefaultAsync();

                    await using var transaction = await _context.Database.BeginTransactionAsync();

                    Wallet? studentWallet = null, teacherWallet = null;
                    decimal commissionRate = 0m, commission = 0m, instructorNet = 0m;

                    if (fee > 0)
                    {
                        if (studentUser == null || teacherUser == null) { attempt.Message = Messages.Failed; return attempt; }

                        studentWallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == studentUser);
                        if (studentWallet == null) { attempt.Message = "رصيد المحفظة غير كافٍ"; return attempt; }

                        commissionRate = await _context.PlatformCommissionSettings
                            .Where(s => s.IsActive).Select(s => (decimal?)s.CommissionPercentage)
                            .FirstOrDefaultAsync() ?? DefaultCommissionPercentage;
                        commission = Math.Round(fee * commissionRate / 100m, 2);
                        instructorNet = fee - commission;

                        // خصم ذري: ينجح فقط إذا الرصيد كافٍ (يمنع السباق بين طلبين)
                        var walletId = studentWallet.Id;
                        var debited = await _context.Wallets
                            .Where(w => w.Id == walletId && w.Balance >= fee)
                            .ExecuteUpdateAsync(s => s.SetProperty(w => w.Balance, w => w.Balance - fee));
                        if (debited == 0) { attempt.Message = "رصيد المحفظة غير كافٍ"; return attempt; }

                        teacherWallet = await _context.Wallets.SingleOrDefaultAsync(w => w.UserId == teacherUser);
                        if (teacherWallet == null)
                        {
                            teacherWallet = new Wallet { UserId = teacherUser, Balance = 0 };
                            SetCreatedFields(teacherWallet, userId);
                            await _context.Wallets.AddAsync(teacherWallet);
                            await _context.SaveChangesAsync();
                        }
                        var teacherWalletId = teacherWallet.Id;
                        await _context.Wallets.Where(w => w.Id == teacherWalletId)
                            .ExecuteUpdateAsync(s => s.SetProperty(w => w.Balance, w => w.Balance + instructorNet));
                    }

                    // التسجيل
                    var enrollment = new Enrollment
                    {
                        StudentId = current.StudentId,
                        GroupId = current.GroupId,
                        CourseId = current.CourseId,
                        JoinRequestId = current.Id,
                        FeeAmount = fee,
                        EnrollmentDate = DateTime.Now,
                        Status = EnrollmentStatus.Active
                    };
                    SetCreatedFields(enrollment, userId);
                    await _context.Enrollments.AddAsync(enrollment);
                    await _context.SaveChangesAsync(); // للحصول على enrollment.Id

                    if (fee > 0)
                    {
                        await _context.WalletTransactions.AddAsync(new WalletTransaction
                        {
                            WalletId = studentWallet!.Id,
                            Direction = WalletTransactionDirection.Out,
                            Type = WalletTransactionType.EnrollmentDeduction,
                            Amount = fee,
                            Status = WalletTransactionStatus.Completed,
                            Description = $"رسوم تسجيل - {course!.Title}",
                            RelatedEntityType = nameof(Enrollment),
                            RelatedEntityId = enrollment.Id,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        });

                        await _context.WalletTransactions.AddAsync(new WalletTransaction
                        {
                            WalletId = teacherWallet!.Id,
                            Direction = WalletTransactionDirection.In,
                            Type = WalletTransactionType.InstructorCredit,
                            Amount = instructorNet,
                            Status = WalletTransactionStatus.Completed,
                            Description = $"صافي رسوم تسجيل بعد العمولة - {course.Title}",
                            RelatedEntityType = nameof(Enrollment),
                            RelatedEntityId = enrollment.Id,
                            CreatedBy = userId,
                            CreatedOn = DateTime.Now
                        });

                        var ledger = new PlatformRevenueLedger
                        {
                            EnrollmentId = enrollment.Id,
                            CommissionAmount = commission,
                            CommissionRateApplied = commissionRate
                        };
                        SetCreatedFields(ledger, userId);
                        await _context.PlatformRevenueLedgers.AddAsync(ledger);
                    }

                    current.Status = JoinRequestStatus.Approved;
                    current.DecisionBy = userId;
                    current.DecisionOn = DateTime.Now;
                    SetUpdatedFields(current, userId);

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    attempt.Success = true;
                    attempt.Message = Messages.Success;
                    attempt.ReturnId = enrollment.Id;
                    return attempt;
                });
            }
            catch (Exception)
            {
                result.Message = Messages.Failed;
                return result;
            }

            if (outcome.Success && studentUserId != null)
                await _notifications.CreateAsync(studentUserId, "تم قبول طلب الانضمام",
                    "تم قبول طلبك وخصم الرسوم من محفظتك.", NotificationType.JoinRequest,
                    nameof(Enrollment), outcome.ReturnId);

            return outcome;
        }
    }
}