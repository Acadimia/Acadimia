using Acadimia.Api.Helper.Authorization;
using Acadimia.Data.Enums;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Bookings;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Bookings;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService) => _bookingService = bookingService;

        [HttpPost] // FR-T05
        public async Task<OperationResult> Submit(BookingInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return result;
            }
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _bookingService.SubmitBookingRequestAsync(studentId, input);
        }

        [HttpPost] // FR-T10 + FR-T06
        public async Task<OperationResult> Decide(BookingDecisionDto input)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _bookingService.DecideBookingAsync(userId, input);
        }

        [HttpPost] // student or owning teacher; FR-S14 rules (deadline + refund) are applied in the service
        public async Task<OperationResult> Cancel(int bookingId, string? reason = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _bookingService.CancelBookingAsync(userId, bookingId, reason);
        }

        [HttpPost]
        public async Task<OperationResult> Complete(int bookingId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _bookingService.CompleteBookingAsync(userId, bookingId);
        }

        [HttpPost] // FR-T11
        public async Task<OperationResult> Rate(RateTeacherDto input)
        {
            var studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _bookingService.RateTeacherAsync(studentId, input);
        }

        [HttpGet]
        public async Task<IActionResult> MyBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _bookingService.GetMyBookingsAsync(userId));
        }

        [HttpGet]
        public async Task<IActionResult> TeacherBookings(BookingStatus? status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _bookingService.GetTeacherBookingsAsync(userId, status));
        }

        // ---- FR-S15: instructor side of reschedule requests ----

        [HttpGet]
        [RequireUserTypes(UserTypeIds.Teacher)]
        public async Task<IActionResult> TeacherRescheduleRequests(RescheduleRequestStatus? status = null)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return Ok(await _bookingService.GetTeacherRescheduleRequestsAsync(userId, status));
        }

        [HttpPost]
        [RequireUserTypes(UserTypeIds.Teacher)]
        public async Task<OperationResult> DecideReschedule(RescheduleDecisionDto input)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return await _bookingService.DecideRescheduleAsync(userId, input);
        }
    }
}
