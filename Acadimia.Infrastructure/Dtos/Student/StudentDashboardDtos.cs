using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.Student
{
    public class StudentDashboardDto
    {
        public string StudentName { get; set; } = string.Empty;
        public int ActiveCoursesCount { get; set; }
        public int UpcomingLessonsCount { get; set; }
        public int PendingBookingsCount { get; set; }
        public int ConfirmedBookingsCount { get; set; }
        public decimal? AttendanceRatePercent { get; set; }
        public decimal? AverageExamScorePercent { get; set; }
        public decimal WalletBalance { get; set; }
        public int UnreadNotificationsCount { get; set; }
    }

    public class StudentBookingDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public CourseDeliveryType TeachingMode { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek Day => Date.DayOfWeek;
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public BookingStatus Status { get; set; }
        public DateTime? PaidOn { get; set; }
        public string? StudentNote { get; set; }
        public string? RejectionReason { get; set; }
    }

    public class StudentScheduleItemDto
    {
        public int LessonId { get; set; }
        public int? CourseId { get; set; }
        public int? GroupId { get; set; }
        public string? CourseName { get; set; }
        public string? TeacherName { get; set; }
        public string Topic { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public DayOfWeek Day => Date.DayOfWeek;
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public CourseDeliveryType? TeachingMode { get; set; }
        public string? Room { get; set; }
        public MeetingPlatform? MeetingPlatform { get; set; }
        public string? MeetingUrl { get; set; }
        public string? MeetingInstructions { get; set; }
    }

    public class StudentAttendanceDto
    {
        public DateTime SessionDate { get; set; }
        public int GroupId { get; set; }
        public string? GroupName { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
    }

    public class StudentExamResultDto
    {
        public int ExamId { get; set; }
        public string? ExamTitle { get; set; }
        public DateTime ExamDate { get; set; }
        public decimal ScoreObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal Percentage { get; set; }
        public string? Feedback { get; set; }
    }

    public class StudentNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public int? RelatedEntityId { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class StudentWalletDto
    {
        public decimal Balance { get; set; }
        public List<WalletTransactionItemDto> Transactions { get; set; } = new();
    }

    public class WalletTransactionItemDto
    {
        public int Id { get; set; }
        public WalletTransactionDirection Direction { get; set; }
        public WalletTransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public WalletTransactionStatus Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}