using Acadimia.Data.Enums;
using Acadimia.Infrastructure.Dtos.Bookings;

namespace Acadimia.Infrastructure.Services.Bookings
{
    public interface IBookingService
    {
        Task<OperationResult> SubmitBookingRequestAsync(string studentId, BookingInputDto input);
        Task<OperationResult> DecideBookingAsync(string userId, BookingDecisionDto input);
        Task<OperationResult> CancelBookingAsync(string userId, int bookingId, string? reason = null);
        Task<OperationResult> CompleteBookingAsync(string userId, int bookingId);
        Task<OperationResult> RateTeacherAsync(string studentId, RateTeacherDto input);
        Task<List<BookingDto>> GetMyBookingsAsync(string userId);
        Task<List<BookingDto>> GetTeacherBookingsAsync(string userId, BookingStatus? status);

        // FR-S15
        Task<OperationResult> RequestRescheduleAsync(string studentId, RescheduleRequestInputDto input);
        Task<OperationResult> DecideRescheduleAsync(string teacherUserId, RescheduleDecisionDto input);
        Task<List<RescheduleRequestDto>> GetMyRescheduleRequestsAsync(string studentId);
        Task<List<RescheduleRequestDto>> GetTeacherRescheduleRequestsAsync(string teacherUserId, RescheduleRequestStatus? status);
    }
}
