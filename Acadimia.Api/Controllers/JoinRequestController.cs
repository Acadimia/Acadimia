using Acadimia.Api.Helper.Authorization;
using Acadimia.Data.Enums;
using Acadimia.Infrastructure.Dtos.JoinRequests;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.JoinRequests;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
{
    public class JoinRequestController : BaseController
    {
        private readonly IJoinRequestService _service;
        public JoinRequestController(IJoinRequestService service) => _service = service;

        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        [HttpPost]
        [RequireUserTypes(UserTypeIds.Student)]
        public async Task<OperationResult> Submit(JoinRequestInputDto input)
            => await _service.SubmitAsync(UserId, input);

        [HttpGet]
        [RequireUserTypes(UserTypeIds.Student)]
        public async Task<IActionResult> MyRequests() => Ok(await _service.GetMyRequestsAsync(UserId));

        [HttpGet]
        [RequireUserTypes(UserTypeIds.Admin, UserTypeIds.Teacher)]
        public async Task<IActionResult> Pending() => Ok(await _service.GetPendingAsync(UserId));

        [HttpPost]
        [RequireUserTypes(UserTypeIds.Admin, UserTypeIds.Teacher)]
        public async Task<OperationResult> Decide(JoinRequestDecisionDto input)
            => await _service.DecideAsync(UserId, input);
    }
}