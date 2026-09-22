using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.Students
{
    // ---------- FR-S05 / S06 / S07 ----------

    public class StudentScheduleFilterDto
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public int? CourseId { get; set; }
        public int? TeacherId { get; set; }
        public CourseDeliveryType? Mode { get; set; }
    }

    // One row of "My Schedule": either a course/group lesson or a confirmed tutoring booking.
    public class StudentScheduleItemDto
    {
        public string Kind { get; set; } = "";              // "Lesson" | "Booking"
        public int Id { get; set; }                          // LessonId or BookingId
        public int? LessonNumber { get; set; }
        public string Topic { get; set; } = "";
        public string? CourseTitle { get; set; }
        public string? TeacherName { get; set; }
        public CourseDeliveryType? Mode { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public string Status { get; set; } = "";

        // In-person only
        public string? Room { get; set; }

        // Online only (FR-S07)
        public MeetingPlatform? MeetingPlatform { get; set; }
        public string? MeetingUrl { get; set; }
        // "Join" is offered only for an online, still-scheduled lesson with a valid meeting link.
        public bool CanJoin { get; set; }
    }

    public class StudentLessonDetailDto
    {
        public StudentScheduleItemDto Lesson { get; set; } = new();
        public string? GroupName { get; set; }
        public string? MeetingInstructions { get; set; }     // online only
        public bool? LocationAvailable { get; set; }         // in-person only
        public string? CancellationReason { get; set; }
    }

    // ---------- FR-S08 ----------

    public class StudentGroupScheduleDayDto
    {
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
    }

    public class StudentGroupInfoDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; } = "";
        public DateTime? CourseStartDate { get; set; }
        public DateTime? CourseEndDate { get; set; }
        public int LessonDurationMinutes { get; set; }
        public List<StudentGroupScheduleDayDto> Days { get; set; } = new();
    }

    public class StudentCourseDetailDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = "";
        public string? Description { get; set; }
        public CourseDeliveryType DeliveryType { get; set; }
        public string? TeacherName { get; set; }
        public string? SubjectName { get; set; }
        public List<StudentGroupInfoDto> Groups { get; set; } = new();
        public List<StudentScheduleItemDto> Lessons { get; set; } = new();
    }

    // ---------- FR-S09 ----------

    public class StudentAttendanceRowDto
    {
        public DateTime SessionDate { get; set; }
        public string GroupName { get; set; } = "";
        public string? CourseTitle { get; set; }
        public AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
    }

    public class StudentAttendanceDto
    {
        // null = no attendance records, so no rate can be calculated.
        public decimal? AttendanceRatePercent { get; set; }
        public int TotalSessions { get; set; }
        public int Present { get; set; }
        public int Absent { get; set; }
        public int Late { get; set; }
        public int Excused { get; set; }
        public List<StudentAttendanceRowDto> Records { get; set; } = new();
    }

    // ---------- FR-S10 ----------

    public class StudentExamResultDto
    {
        public int ExamId { get; set; }
        public string ExamTitle { get; set; } = "";
        public DateTime ExamDate { get; set; }
        public string? CourseTitle { get; set; }
        public decimal ScoreObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal? Percentage { get; set; }             // null when TotalMarks is 0
        public string? Feedback { get; set; }
    }

    // ---------- FR-S11 ----------

    public class StudentProgressDto
    {
        public decimal? AttendanceRatePercent { get; set; }      // null = unavailable
        public decimal? AverageExamScorePercent { get; set; }    // null = unavailable
        public int AttendanceSessions { get; set; }
        public int ExamsTaken { get; set; }
    }

    // ---------- FR-S12 ----------

    public class StudentNotificationDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Message { get; set; } = "";
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    // ---------- FR-S13 ----------

    public class BookingPaymentDto
    {
        public int BookingId { get; set; }
        public BookingStatus BookingStatus { get; set; }
        public decimal Price { get; set; }
        public BookingPaymentStatus PaymentStatus { get; set; }
        public DateTime? PaidOn { get; set; }
        public decimal WalletBalance { get; set; }
        public bool HasSufficientBalance { get; set; }
        // true = show "top up your wallet" (booking still waiting for payment and the balance is short)
        public bool NeedsTopUp { get; set; }
    }

    // ---------- FR-S01 ----------

    public class ActiveCourseDto
    {
        public int EnrollmentId { get; set; }
        public int? CourseId { get; set; }
        public string? CourseTitle { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public string? TeacherName { get; set; }
        public CourseDeliveryType? DeliveryType { get; set; }
    }

    public class StudentDashboardDto
    {
        public string StudentName { get; set; } = "";
        // false = the login account is not linked to a Student profile yet,
        // so enrollment-based sections (courses, lessons, attendance, exams) are empty.
        public bool HasStudentProfile { get; set; }
        public List<ActiveCourseDto> ActiveCourses { get; set; } = new();
        public List<StudentScheduleItemDto> UpcomingSchedule { get; set; } = new();
        public int PendingRequestsCount { get; set; }
        public int ConfirmedBookingsCount { get; set; }
        public StudentProgressDto Progress { get; set; } = new();
        public decimal WalletBalance { get; set; }
        public int UnreadNotificationsCount { get; set; }
    }
}
