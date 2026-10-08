using Acadimia.Api.Helper.Authorization;
using Acadimia.Data.Enums;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos.Parent;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Parent;
using Microsoft.AspNetCore.Mvc;

namespace Acadimia.Api.Controllers
{
    // FR-P09 / FR-P10: إدارة ربط أولياء الأمور بالطلاب وصلة القرابة (للأدمن فقط)
    [RequireUserTypes(UserTypeIds.Admin)]
    public class ParentLinkController : BaseController
    {
        private readonly IParentService _parentService;
        public ParentLinkController(IParentService parentService) => _parentService = parentService;

        private OperationResult? ValidationFailure()
        {
            if (ModelState.IsValid) return null;
            return new OperationResult(false, string.Join("<br>",
                ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
        }

        [HttpGet] // قائمة صلات القرابة (ثوابت Kinship) للـ dropdown
        public async Task<IActionResult> RelationTypes() => Ok(await _parentService.GetRelationTypesAsync());

        [HttpGet]
        public async Task<IActionResult> List(int? studentId = null, string? parentUserId = null)
            => Ok(await _parentService.GetLinksAsync(studentId, parentUserId));

        [HttpPost] // FR-P09
        public async Task<OperationResult> Link(ParentLinkInputDto input)
        {
            var invalid = ValidationFailure();
            if (invalid != null) return invalid;
            return await _parentService.LinkChildAsync(input.ParentUserId, input.StudentId,
                input.RelationTypeId, input.IsPrimaryContact);
        }

        [HttpPost] // FR-P10
        public async Task<OperationResult> UpdateRelation(ParentLinkUpdateDto input)
        {
            var invalid = ValidationFailure();
            if (invalid != null) return invalid;
            return await _parentService.UpdateRelationAsync(input);
        }

        [HttpDelete]
        public async Task<OperationResult> Unlink(int linkId) => await _parentService.UnlinkAsync(linkId);
    }
}
