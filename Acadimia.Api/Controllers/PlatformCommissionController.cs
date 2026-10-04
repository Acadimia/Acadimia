using Acadimia.Api.Helper.Authorization;
using Acadimia.Data.Enums;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Commission;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.PlatformCommission;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
{
    [RequireUserTypes(UserTypeIds.Admin)]
    public class PlatformCommissionController : BaseController
    {
        private readonly IPlatformCommissionService _service;
        public PlatformCommissionController(IPlatformCommissionService service) => _service = service;

        [HttpGet] // النسبة الحالية
        public async Task<IActionResult> Current() => Ok(await _service.GetCurrentAsync());

        [HttpPost] // سجل تغييرات النسبة
        public async Task<IActionResult> History([FromBody] DataTableRequestDto? request = null)
        {
            request ??= new DataTableRequestDto();
            var result = await _service.GetHistoryAsync(request);
            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        [HttpPost] // تعديل النسبة (0-100)
        public async Task<OperationResult> Set(SetCommissionInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return result;
            }

            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            return await _service.SetCommissionAsync(adminId, input);
        }
    }
}