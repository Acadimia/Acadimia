using Acadimia.Infrastructure.Dtos.JoinRequests;

namespace Acadimia.Infrastructure.Services.JoinRequests
{
    public interface IJoinRequestService
    {
        Task<OperationResult> SubmitAsync(string userId, JoinRequestInputDto input);
        Task<List<JoinRequestDto>> GetMyRequestsAsync(string userId);
        Task<List<JoinRequestDto>> GetPendingAsync(string userId);
        Task<OperationResult> DecideAsync(string userId, JoinRequestDecisionDto input);
    }
}