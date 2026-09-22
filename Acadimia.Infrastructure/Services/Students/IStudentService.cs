using Acadimia.Data.Enums;
using Acadimia.Infrastructure.Dtos.Bookings;
using Acadimia.Infrastructure.Dtos.Students;

namespace Acadimia.Infrastructure.Services.Students
{
    // Read side of the student module (SRS 2.2.11). Every method takes the login user id and
    // only ever returns records that belong to that student.
    public interface IStudentService
    {
        Task<StudentDashboardDto> GetDashboardAsync(string userId);                                   // FR-S01

        Task<List<BookingDto>> GetMyRequestsAsync(string userId, BookingStatus? status);              // FR-S02 (Pending/Accepted/Rejected/Cancelled)
        Task<BookingDto?> GetBookingAsync(string userId, int bookingId);                              // FR-S03
        Task<List<BookingDto>> GetMyConfirmedBookingsAsync(string userId);                            // FR-S04 (Confirmed/Completed)

        Task<List<StudentScheduleItemDto>> GetScheduleAsync(string userId, StudentScheduleFilterDto filter);  // FR-S05
        Task<StudentLessonDetailDto?> GetLessonDetailAsync(string userId, int lessonId);              // FR-S06 / FR-S07
        Task<StudentCourseDetailDto?> GetCourseDetailAsync(string userId, int courseId);              // FR-S08

        Task<StudentAttendanceDto> GetAttendanceAsync(string userId, DateTime? from, DateTime? to, int? courseId);  // FR-S09
        Task<List<StudentExamResultDto>> GetExamResultsAsync(string userId, int? courseId);           // FR-S10
        Task<StudentProgressDto> GetProgressAsync(string userId);                                     // FR-S11

        Task<PagedResultDto<List<StudentNotificationDto>>> GetNotificationsAsync(string userId, bool unreadOnly, int skip, int pageSize); // FR-S12
        Task<OperationResult> MarkNotificationReadAsync(string userId, int notificationId);           // FR-S12

        Task<BookingPaymentDto?> GetBookingPaymentAsync(string userId, int bookingId);                // FR-S13
    }
}
