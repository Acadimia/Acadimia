using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Bookings;
using Acadimia.Infrastructure.Dtos.Students;
using Acadimia.Infrastructure.Services.Bookings;
using Acadimia.Infrastructure.Services.Ownership;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Acadimia.Infrastructure.Services.Students
{
    // Flat row that a Lesson is projected into, so the projection runs in SQL.
    internal sealed class StudentLessonRow
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public int OrderIndex { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public LessonStatus Status { get; set; }
        public MeetingPlatform? MeetingPlatform { get; set; }
        public string? MeetingUrl { get; set; }
        public string? MeetingInstructions { get; set; }
        public string? Room { get; set; }
        public string? CancellationReason { get; set; }
        public CourseDeliveryType? Mode { get; set; }
        public string? TeacherName { get; set; }
        public string? CourseTitle { get; set; }
        public string? GroupName { get; set; }
    }

    public class StudentService : BaseService, IStudentService
    {
        private readonly IOwnershipService _ownership;

        public StudentService(ApplicationDbContext context, UserManager<User> userManager,
            IHttpContextAccessor httpContextAccessor, IOwnershipService ownership)
            : base(context, userManager, httpContextAccessor)
        {
            _ownership = ownership;
        }

        // A lesson can belong to a Group or directly to a Course; the student sees it through an
        // active Enrollment on either one (same matching rule LessonService.CancelAsync uses).
        private static readonly Expression<Func<Lesson, StudentLessonRow>> LessonProjection = l => new StudentLessonRow
        {
            Id = l.Id,
            Title = l.Title,
            OrderIndex = l.OrderIndex,
            ScheduledDate = l.ScheduledDate,
            StartTime = l.StartTime,
            DurationMinutes = l.DurationMinutes,
            Status = l.Status,
            MeetingPlatform = l.MeetingPlatform,
            MeetingUrl = l.MeetingUrl,
            MeetingInstructions = l.MeetingInstructions,
            Room = l.Room,
            CancellationReason = l.CancellationReason,
            Mode = l.Course != null
                ? (CourseDeliveryType?)l.Course.DeliveryType
                : (l.Group != null && l.Group.Course != null ? (CourseDeliveryType?)l.Group.Course.DeliveryType : null),
            TeacherName = l.Course != null
                ? (l.Course.Teacher.User != null ? l.Course.Teacher.User.Name : null)
                : (l.Group != null && l.Group.Teacher.User != null ? l.Group.Teacher.User.Name : null),
            CourseTitle = l.Course != null
                ? l.Course.Title
                : (l.Group != null && l.Group.Course != null ? l.Group.Course.Title : null),
            GroupName = l.Group != null ? l.Group.Name : null
        };

        private IQueryable<Lesson> LessonsForStudent(int studentId) =>
            _context.Lessons.Where(l => _context.Enrollments.Any(e =>
                e.StudentId == studentId
                && e.Status == EnrollmentStatus.Active
                && ((l.GroupId != null && e.GroupId == l.GroupId)
                    || (l.CourseId != null && e.CourseId == l.CourseId))));

        private static bool IsValidMeetingUrl(string? url) =>
            Uri.TryCreate(url, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);

        private static StudentScheduleItemDto ToScheduleItem(StudentLessonRow r)
        {
            var isOnline = r.Mode == CourseDeliveryType.Online;
            return new StudentScheduleItemDto
            {
                Kind = "Lesson",
                Id = r.Id,
                LessonNumber = r.OrderIndex,
                Topic = r.Title,
                CourseTitle = r.CourseTitle,
                TeacherName = r.TeacherName,
                Mode = r.Mode,
                Date = r.ScheduledDate.Date,
                Day = r.ScheduledDate.DayOfWeek,
                StartTime = r.StartTime,
                DurationMinutes = r.DurationMinutes,
                Status = r.Status.ToString(),
                // In-person shows the room; online shows the meeting platform/link (FR-S06 / FR-S07).
                Room = isOnline ? null : r.Room,
                MeetingPlatform = isOnline ? r.MeetingPlatform : null,
                MeetingUrl = isOnline ? r.MeetingUrl : null,
                CanJoin = isOnline && r.Status == LessonStatus.Scheduled && IsValidMeetingUrl(r.MeetingUrl)
            };
        }

        // ==================== FR-S01 ====================

        public async Task<StudentDashboardDto> GetDashboardAsync(string userId)
        {
            var dto = new StudentDashboardDto();

            dto.StudentName = await _context.Users.Where(u => u.Id == userId).Select(u => u.Name).FirstOrDefaultAsync() ?? "";

            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            dto.HasStudentProfile = studentId != null;

            if (studentId != null)
            {
                var sid = studentId.Value;
                dto.ActiveCourses = await _context.Enrollments
                    .Where(e => e.StudentId == sid && e.Status == EnrollmentStatus.Active)
                    .Select(e => new ActiveCourseDto
                    {
                        EnrollmentId = e.Id,
                        CourseId = e.CourseId != null ? e.CourseId : (e.Group != null ? e.Group.CourseId : null),
                        CourseTitle = e.Course != null
                            ? e.Course.Title
                            : (e.Group != null && e.Group.Course != null ? e.Group.Course.Title : null),
                        GroupId = e.GroupId,
                        GroupName = e.Group != null ? e.Group.Name : null,
                        TeacherName = e.Course != null
                            ? (e.Course.Teacher.User != null ? e.Course.Teacher.User.Name : null)
                            : (e.Group != null && e.Group.Teacher.User != null ? e.Group.Teacher.User.Name : null),
                        DeliveryType = e.Course != null
                            ? (CourseDeliveryType?)e.Course.DeliveryType
                            : (e.Group != null && e.Group.Course != null ? (CourseDeliveryType?)e.Group.Course.DeliveryType : null)
                    }).ToListAsync();
            }

            var now = DateTime.Now;
            var upcoming = await GetScheduleAsync(userId, new StudentScheduleFilterDto { From = DateTime.Today });
            dto.UpcomingSchedule = upcoming
                .Where(i => (i.Status == nameof(LessonStatus.Scheduled) || i.Status == nameof(BookingStatus.Confirmed))
                            && i.Date.Date + i.StartTime >= now)
                .Take(5)
                .ToList();

            dto.PendingRequestsCount = await _context.Bookings.CountAsync(b => b.StudentId == userId
                && (b.Status == BookingStatus.Pending || b.Status == BookingStatus.Accepted));
            dto.ConfirmedBookingsCount = await _context.Bookings.CountAsync(b => b.StudentId == userId
                && b.Status == BookingStatus.Confirmed);

            dto.Progress = await BuildProgressAsync(studentId);

            dto.WalletBalance = await _context.Wallets.Where(w => w.UserId == userId)
                .Select(w => w.Balance).FirstOrDefaultAsync();
            dto.UnreadNotificationsCount = await _context.Notifications.CountAsync(n => n.UserId == userId && !n.IsRead);

            return dto;
        }

        // ==================== FR-S02 / S03 / S04 ====================

        public async Task<List<BookingDto>> GetMyRequestsAsync(string userId, BookingStatus? status)
        {
            var requestStatuses = new[] { BookingStatus.Pending, BookingStatus.Accepted, BookingStatus.Rejected, BookingStatus.Cancelled };
            var query = _context.Bookings.Where(b => b.StudentId == userId && requestStatuses.Contains(b.Status));

            if (status.HasValue)
            {
                var wanted = status.Value;
                query = query.Where(b => b.Status == wanted);
            }

            return await query.OrderByDescending(b => b.CreatedOn).Select(BookingProjections.ToDto).ToListAsync();
        }

        public async Task<BookingDto?> GetBookingAsync(string userId, int bookingId) =>
            await _context.Bookings.Where(b => b.Id == bookingId && b.StudentId == userId)
                .Select(BookingProjections.ToDto)
                .FirstOrDefaultAsync();

        public async Task<List<BookingDto>> GetMyConfirmedBookingsAsync(string userId)
        {
            var confirmedStatuses = new[] { BookingStatus.Confirmed, BookingStatus.Completed };
            return await _context.Bookings
                .Where(b => b.StudentId == userId && confirmedStatuses.Contains(b.Status))
                .OrderByDescending(b => b.Date)
                .Select(BookingProjections.ToDto)
                .ToListAsync();
        }

        // ==================== FR-S05 ====================

        public async Task<List<StudentScheduleItemDto>> GetScheduleAsync(string userId, StudentScheduleFilterDto filter)
        {
            var items = new List<StudentScheduleItemDto>();
            var fromDate = filter.From?.Date;
            var toExclusive = filter.To?.Date.AddDays(1);

            // ---- lessons of the student's active enrollments ----
            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId != null)
            {
                var lessons = LessonsForStudent(studentId.Value);

                if (fromDate.HasValue)
                {
                    var f = fromDate.Value;
                    lessons = lessons.Where(l => l.ScheduledDate >= f);
                }
                if (toExclusive.HasValue)
                {
                    var t = toExclusive.Value;
                    lessons = lessons.Where(l => l.ScheduledDate < t);
                }
                if (filter.CourseId.HasValue)
                {
                    var cid = filter.CourseId.Value;
                    lessons = lessons.Where(l => l.CourseId == cid || (l.Group != null && l.Group.CourseId == cid));
                }
                if (filter.TeacherId.HasValue)
                {
                    var tid = filter.TeacherId.Value;
                    lessons = lessons.Where(l => (l.Course != null && l.Course.TeacherId == tid)
                                                 || (l.Group != null && l.Group.TeacherId == tid));
                }

                var rows = await lessons.Select(LessonProjection).ToListAsync();
                items.AddRange(rows.Select(ToScheduleItem));
            }

            // ---- confirmed tutoring bookings (they have no course, so a course filter excludes them) ----
            if (!filter.CourseId.HasValue)
            {
                var confirmedStatuses = new[] { BookingStatus.Confirmed, BookingStatus.Completed };
                var bookings = _context.Bookings.Where(b => b.StudentId == userId && confirmedStatuses.Contains(b.Status));

                if (fromDate.HasValue)
                {
                    var f = fromDate.Value;
                    bookings = bookings.Where(b => b.Date >= f);
                }
                if (toExclusive.HasValue)
                {
                    var t = toExclusive.Value;
                    bookings = bookings.Where(b => b.Date < t);
                }
                if (filter.TeacherId.HasValue)
                {
                    var tid = filter.TeacherId.Value;
                    bookings = bookings.Where(b => b.TeacherId == tid);
                }

                var bookingRows = await bookings.Select(b => new
                {
                    b.Id,
                    b.Date,
                    b.StartTime,
                    b.DurationMinutes,
                    b.Status,
                    b.TeachingMode,
                    Subject = b.Subject != null ? b.Subject.Name : null,
                    TeacherName = b.Teacher.User != null ? b.Teacher.User.Name : null
                }).ToListAsync();

                items.AddRange(bookingRows.Select(b => new StudentScheduleItemDto
                {
                    Kind = "Booking",
                    Id = b.Id,
                    Topic = b.Subject ?? "جلسة خصوصية",
                    TeacherName = b.TeacherName,
                    Mode = b.TeachingMode,
                    Date = b.Date.Date,
                    Day = b.Date.DayOfWeek,
                    StartTime = b.StartTime,
                    DurationMinutes = b.DurationMinutes,
                    Status = b.Status.ToString()
                }));
            }

            if (filter.Mode.HasValue)
                items = items.Where(i => i.Mode == filter.Mode).ToList();

            return items.OrderBy(i => i.Date.Date + i.StartTime).ToList();
        }

        // ==================== FR-S06 / S07 ====================

        public async Task<StudentLessonDetailDto?> GetLessonDetailAsync(string userId, int lessonId)
        {
            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId == null) return null;

            var row = await LessonsForStudent(studentId.Value)
                .Where(l => l.Id == lessonId)
                .Select(LessonProjection)
                .FirstOrDefaultAsync();
            if (row == null) return null;

            var isOnline = row.Mode == CourseDeliveryType.Online;
            return new StudentLessonDetailDto
            {
                Lesson = ToScheduleItem(row),
                GroupName = row.GroupName,
                MeetingInstructions = isOnline ? row.MeetingInstructions : null,
                LocationAvailable = isOnline ? null : !string.IsNullOrWhiteSpace(row.Room),
                CancellationReason = row.CancellationReason
            };
        }

        // ==================== FR-S08 ====================

        public async Task<StudentCourseDetailDto?> GetCourseDetailAsync(string userId, int courseId)
        {
            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId == null) return null;
            var sid = studentId.Value;

            // Enrolled either in the course itself or in one of its groups.
            var enrollments = await _context.Enrollments
                .Where(e => e.StudentId == sid
                            && e.Status == EnrollmentStatus.Active
                            && (e.CourseId == courseId || (e.Group != null && e.Group.CourseId == courseId)))
                .Select(e => new { e.CourseId, e.GroupId })
                .ToListAsync();
            if (enrollments.Count == 0) return null;

            var course = await _context.Courses.Where(c => c.Id == courseId)
                .Select(c => new
                {
                    c.Id,
                    c.Title,
                    c.Description,
                    c.DeliveryType,
                    TeacherName = c.Teacher.User != null ? c.Teacher.User.Name : null,
                    SubjectName = c.Subject != null ? c.Subject.Name : null
                }).FirstOrDefaultAsync();
            if (course == null) return null;

            var groupIds = enrollments.Where(e => e.GroupId != null).Select(e => e.GroupId!.Value).Distinct().ToList();

            var groups = await _context.Groups.Where(g => groupIds.Contains(g.Id))
                .Select(g => new { g.Id, g.Name, g.CourseStartDate, g.CourseEndDate, g.DefaultLessonDurationMinutes })
                .ToListAsync();
            var days = await _context.GroupScheduleDays.Where(d => groupIds.Contains(d.GroupId)).ToListAsync();

            var lessonRows = await LessonsForStudent(sid)
                .Where(l => l.CourseId == courseId || (l.Group != null && l.Group.CourseId == courseId))
                .OrderBy(l => l.ScheduledDate).ThenBy(l => l.StartTime)
                .Select(LessonProjection)
                .ToListAsync();

            return new StudentCourseDetailDto
            {
                CourseId = course.Id,
                Title = course.Title,
                Description = course.Description,
                DeliveryType = course.DeliveryType,
                TeacherName = course.TeacherName,
                SubjectName = course.SubjectName,
                Groups = groups.Select(g => new StudentGroupInfoDto
                {
                    GroupId = g.Id,
                    Name = g.Name,
                    CourseStartDate = g.CourseStartDate,
                    CourseEndDate = g.CourseEndDate,
                    LessonDurationMinutes = g.DefaultLessonDurationMinutes,
                    Days = days.Where(d => d.GroupId == g.Id)
                        .OrderBy(d => d.DayOfWeek).ThenBy(d => d.StartTime)
                        .Select(d => new StudentGroupScheduleDayDto { Day = d.DayOfWeek, StartTime = d.StartTime })
                        .ToList()
                }).ToList(),
                Lessons = lessonRows.Select(ToScheduleItem).ToList()
            };
        }

        // ==================== FR-S09 ====================

        public async Task<StudentAttendanceDto> GetAttendanceAsync(string userId, DateTime? from, DateTime? to, int? courseId)
        {
            var dto = new StudentAttendanceDto();
            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId == null) return dto;
            var sid = studentId.Value;

            var query = _context.Attendances.Where(a => a.StudentId == sid);
            if (from.HasValue)
            {
                var f = from.Value.Date;
                query = query.Where(a => a.SessionDate >= f);
            }
            if (to.HasValue)
            {
                var t = to.Value.Date.AddDays(1);
                query = query.Where(a => a.SessionDate < t);
            }
            if (courseId.HasValue)
            {
                var cid = courseId.Value;
                query = query.Where(a => a.Group.CourseId == cid);
            }

            dto.Records = await query.OrderByDescending(a => a.SessionDate)
                .Select(a => new StudentAttendanceRowDto
                {
                    SessionDate = a.SessionDate,
                    GroupName = a.Group.Name,
                    CourseTitle = a.Group.Course != null ? a.Group.Course.Title : null,
                    Status = a.Status,
                    Notes = a.Notes
                }).ToListAsync();

            dto.TotalSessions = dto.Records.Count;
            dto.Present = dto.Records.Count(r => r.Status == AttendanceStatus.Present);
            dto.Absent = dto.Records.Count(r => r.Status == AttendanceStatus.Absent);
            dto.Late = dto.Records.Count(r => r.Status == AttendanceStatus.Late);
            dto.Excused = dto.Records.Count(r => r.Status == AttendanceStatus.Excused);
            // Same formula as FR-P06 / ParentService: present sessions / all recorded sessions.
            dto.AttendanceRatePercent = dto.TotalSessions == 0
                ? (decimal?)null
                : Math.Round((decimal)dto.Present / dto.TotalSessions * 100, 1);

            return dto;
        }

        // ==================== FR-S10 ====================

        public async Task<List<StudentExamResultDto>> GetExamResultsAsync(string userId, int? courseId)
        {
            var studentId = await _ownership.GetStudentIdForUserAsync(userId);
            if (studentId == null) return new List<StudentExamResultDto>();
            var sid = studentId.Value;

            var query = _context.ExamResults.Where(x => x.StudentId == sid);
            if (courseId.HasValue)
            {
                var cid = courseId.Value;
                query = query.Where(x => x.Exam.CourseId == cid || (x.Exam.Group != null && x.Exam.Group.CourseId == cid));
            }

            var rows = await query.OrderByDescending(x => x.Exam.ExamDate)
                .Select(x => new StudentExamResultDto
                {
                    ExamId = x.ExamId,
                    ExamTitle = x.Exam.Title,
                    ExamDate = x.Exam.ExamDate,
                    CourseTitle = x.Exam.Course != null
                        ? x.Exam.Course.Title
                        : (x.Exam.Group != null && x.Exam.Group.Course != null ? x.Exam.Group.Course.Title : null),
                    ScoreObtained = x.ScoreObtained,
                    TotalMarks = x.Exam.TotalMarks,
                    Feedback = x.Feedback
                }).ToListAsync();

            foreach (var row in rows)
                row.Percentage = row.TotalMarks > 0 ? Math.Round(row.ScoreObtained / row.TotalMarks * 100, 1) : (decimal?)null;

            return rows;
        }

        // ==================== FR-S11 ====================

        public async Task<StudentProgressDto> GetProgressAsync(string userId) =>
            await BuildProgressAsync(await _ownership.GetStudentIdForUserAsync(userId));

        private async Task<StudentProgressDto> BuildProgressAsync(int? studentId)
        {
            var dto = new StudentProgressDto();
            if (studentId == null) return dto;
            var sid = studentId.Value;

            var total = await _context.Attendances.CountAsync(a => a.StudentId == sid);
            var present = await _context.Attendances.CountAsync(a => a.StudentId == sid && a.Status == AttendanceStatus.Present);
            dto.AttendanceSessions = total;
            dto.AttendanceRatePercent = total == 0 ? (decimal?)null : Math.Round((decimal)present / total * 100, 1);

            // Exams with TotalMarks = 0 can't produce a percentage (FR-P07 exception flow), so they are skipped.
            var scores = await _context.ExamResults
                .Where(x => x.StudentId == sid && x.Exam.TotalMarks > 0)
                .Select(x => new { x.ScoreObtained, x.Exam.TotalMarks })
                .ToListAsync();
            dto.ExamsTaken = scores.Count;
            dto.AverageExamScorePercent = scores.Count == 0
                ? (decimal?)null
                : Math.Round(scores.Average(x => x.ScoreObtained / x.TotalMarks) * 100, 1);

            return dto;
        }

        // ==================== FR-S12 ====================

        public async Task<PagedResultDto<List<StudentNotificationDto>>> GetNotificationsAsync(string userId, bool unreadOnly, int skip, int pageSize)
        {
            var query = _context.Notifications.Where(n => n.UserId == userId);
            if (unreadOnly)
                query = query.Where(n => !n.IsRead);

            return new PagedResultDto<List<StudentNotificationDto>>
            {
                TotalCount = await query.CountAsync(),
                Data = await query.OrderByDescending(n => n.CreatedOn)
                    .Skip(skip).Take(pageSize)
                    .Select(n => new StudentNotificationDto
                    {
                        Id = n.Id,
                        Title = n.Title,
                        Message = n.Message,
                        Type = n.Type,
                        IsRead = n.IsRead,
                        RelatedEntityType = n.RelatedEntityType,
                        RelatedEntityId = n.RelatedEntityId,
                        CreatedOn = n.CreatedOn
                    }).ToListAsync()
            };
        }

        public async Task<OperationResult> MarkNotificationReadAsync(string userId, int notificationId)
        {
            var result = new OperationResult(false, Messages.Failed);

            var notification = await _context.Notifications
                .SingleOrDefaultAsync(n => n.Id == notificationId && n.UserId == userId);
            if (notification == null) return result;

            if (!notification.IsRead)
            {
                notification.IsRead = true;
                SetUpdatedFields(notification, userId);
                await _context.SaveChangesAsync();
            }

            result.Success = true;
            result.Message = Messages.Success;
            return result;
        }

        // ==================== FR-S13 ====================
        // Balance and transaction history already live in WalletController (MyWallet / GetTransactionHistory);
        // this adds the per-booking payment state the requirement asks for.

        public async Task<BookingPaymentDto?> GetBookingPaymentAsync(string userId, int bookingId)
        {
            var booking = await _context.Bookings
                .Where(b => b.Id == bookingId && b.StudentId == userId)
                .Select(b => new { b.Id, b.Price, b.Status, b.PaidOn })
                .FirstOrDefaultAsync();
            if (booking == null) return null;

            var balance = await _context.Wallets.Where(w => w.UserId == userId)
                .Select(w => w.Balance).FirstOrDefaultAsync();

            var refunded = await _context.WalletTransactions.AnyAsync(t =>
                t.RelatedEntityType == nameof(Booking)
                && t.RelatedEntityId == bookingId
                && t.Type == WalletTransactionType.BookingRefund
                && t.Wallet.UserId == userId);

            var paymentStatus = refunded ? BookingPaymentStatus.Refunded
                : booking.PaidOn != null ? BookingPaymentStatus.Paid
                : BookingPaymentStatus.Unpaid;

            var hasBalance = balance >= booking.Price;
            var waitingForPayment = booking.Status == BookingStatus.Pending || booking.Status == BookingStatus.Accepted;

            return new BookingPaymentDto
            {
                BookingId = booking.Id,
                BookingStatus = booking.Status,
                Price = booking.Price,
                PaymentStatus = paymentStatus,
                PaidOn = booking.PaidOn,
                WalletBalance = balance,
                HasSufficientBalance = hasBalance,
                NeedsTopUp = waitingForPayment && !hasBalance
            };
        }
    }
}
