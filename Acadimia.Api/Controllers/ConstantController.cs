using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services.Constants;
using Acadimia.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Constants;
using Acadimia.Infrastructure.Dtos;

namespace Acadimia.Api.Controllers
{
    public class ConstantController : BaseController
    {
        private readonly IConstantsService _constantsService;
        public ConstantController(IConstantsService constantsService)
        {
            _constantsService = constantsService;
        }

        [HttpPost]
        public async Task<ActionResult<object>> GetAll([FromBody] DataTableRequestDto request)
        {
            var obj = new Constant { Keyword = request.SearchValue };

            var result = await _constantsService.GetAllAsync(new PagedResultRequestDto<Constant>
            {
                SearchValue = obj,
                SortColumn = request.SortColumn,
                SortColumnDirection = request.SortColumnDirection,
                PageSize = request.PageSize,
                Skip = request.Skip
            });

            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        [HttpGet] // returns data for create/edit Constant form
        public async Task<IActionResult> CreateEditModal(int id)
        {
            return Ok(new CreateEditConstant
            {
                Constant = await _constantsService.GetByIdOrDefaultAsync(id),
                Parents = await _constantsService.GetParentsListItemAsync(),
            });
        }

        [HttpPost] // Create Edit Constant
        public async Task<OperationResult> CreateEdit(ConstantInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>  ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return result;
            }

            var entity = new Constant
            {
                Id = input.Id,
                Name = input.Name,
                Comment = input.Comment,
                Icon = input.Icon,
                ParentId = input.ParentId
            };

            return await _constantsService.CreateEditAsync(entity);
        }

        [HttpDelete]
        public async Task<OperationResult> Delete(int id)
        {
            return await _constantsService.DeleteAsync(id);
        }
    }
}