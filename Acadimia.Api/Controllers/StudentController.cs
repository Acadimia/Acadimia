using Acadimia.Api.Helper.Authorization;
using Acadimia.Data.Enums;
using Acadimia.Infrastructure.Dtos.Bookings;
using Acadimia.Infrastructure.Dtos.Students;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Bookings;
using Acadimia.Infrastructure.Services.Students;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
{
    // SRS 2.2.11 - Student Dashboard, Learning & Booking Management (FR-S01..FR-S15).
    // Only logged-in users of type Student can reach any action here (RequireUserTypes also returns 401
    // for anonymous callers), and every service call is scoped to that user's own records.
    [RequireUserTypes(UserTypeIds.Student)]
    public class StudentController : BaseController
    {
        private readonly IStudentService _studentService;
        private readonly IBookingService _bookingService;

        public StudentController(IStudentService studentService, IBookingService bookingService)
        {
            _studentService = studentService;
            _bookingService = bookingService;
        }

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        private IActionResult Denied() => StatusCode(StatusCodes.Status403Forbidden);

        [HttpGet] // FR-S01
        public async Task<IActionResult> Dashboard() => Ok(await _studentService.GetDashboardAsync(UserId));

        // ---------- bookings ----------

        [HttpGet] // FR-S02  "My Requests"
        public async Task<IActionResult> MyRequests(BookingStatus? status = null)
            => Ok(await _studentService.GetMyRequestsAsync(UserId, status));

        [HttpGet] // FR-S03
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _studentService.GetBookingAsync(UserId, id);
            return booking == null ? Denied() : Ok(booking);
        }

        [HttpGet] // FR-S04  "My Bookings"
        public async Task<IActionResult> MyBookings() => Ok(await _studentService.GetMyConfirmedBookingsAsync(UserId));

        [HttpGet] // FR-S13 (per-booking payment state; balance/history: Wallet/MyWallet, Wallet/GetTransactionHistory)
        public async Task<IActionResult> BookingPayment(int bookingId)
        {
            var payment = await _studentService.GetBookingPaymentAsync(UserId, bookingId);
            return payment == null ? Denied() : Ok(payment);
        }

        [HttpPost] // FR-S14
        public async Task<OperationResult> CancelBooking(CancelBookingDto input)
            => await _bookingService.CancelBookingAsync(UserId, input.BookingId, input.Reason);

        [HttpPost] // FR-S15
        public async Task<OperationResult> RequestReschedule(RescheduleRequestInputDto input)
            => await _bookingService.RequestRescheduleAsync(UserId, input);

        [HttpGet] // FR-S15 - track my reschedule requests
        public async Task<IActionResult> MyRescheduleRequests()
            => Ok(await _bookingService.GetMyRescheduleRequestsAsync(UserId));

        // ---------- learning ----------

        [HttpGet] // FR-S05
        public async Task<IActionResult> Schedule([FromQuery] StudentScheduleFilterDto filter)
            => Ok(await _studentService.GetScheduleAsync(UserId, filter));

        [HttpGet] // FR-S06 / FR-S07
        public async Task<IActionResult> GetLesson(int id)
        {
            var lesson = await _studentService.GetLessonDetailAsync(UserId, id);
            return lesson == null ? Denied() : Ok(lesson);
        }

        [HttpGet] // FR-S08
        public async Task<IActionResult> GetCourse(int id)
        {
            var course = await _studentService.GetCourseDetailAsync(UserId, id);
            return course == null ? Denied() : Ok(course);
        }

        // ---------- academic ----------

        [HttpGet] // FR-S09
        public async Task<IActionResult> Attendance(DateTime? from = null, DateTime? to = null, int? courseId = null)
            => Ok(await _studentService.GetAttendanceAsync(UserId, from, to, courseId));

        [HttpGet] // FR-S10
        public async Task<IActionResult> ExamResults(int? courseId = null)
            => Ok(await _studentService.GetExamResultsAsync(UserId, courseId));

        [HttpGet] // FR-S11
        public async Task<IActionResult> Progress() => Ok(await _studentService.GetProgressAsync(UserId));

        // ---------- notifications ----------

        [HttpGet] // FR-S12
        public async Task<IActionResult> Notifications(bool unreadOnly = false, int skip = 0, int pageSize = 20)
        {
            var result = await _studentService.GetNotificationsAsync(
                UserId, unreadOnly, Math.Max(skip, 0), Math.Clamp(pageSize, 1, 100));
            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        [HttpPost] // FR-S12 - mark as read
        public async Task<OperationResult> MarkNotificationRead(int id)
            => await _studentService.MarkNotificationReadAsync(UserId, id);
    }
}
