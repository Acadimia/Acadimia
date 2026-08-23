using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services.Constants;
using Acadimia.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Constants;
using Newtonsoft.Json;
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




        [HttpGet] // Display Create Edit Constant Page
		public async Task<IActionResult> CreateEditModal(int id)
		{
			return PartialView("_CreateEditModal", new CreateEditConstant
			{
				Constant = await _constantsService.GetByIdOrDefaultAsync(id),
				Parents = await _constantsService.GetParentsListItemAsync(),
			});
		}

		[HttpPost] // Create Edit Constant
		public async Task<OperationResult> CreateEdit(Constant input)
		{
			var result = new OperationResult(false, Messages.Invalid);
			if (!ModelState.IsValid)
			{
				var message = string.Join("<br>  ", ModelState.Values
					.SelectMany(v => v.Errors)
					.Select(e => e.ErrorMessage));
				result.Message = message;
				return result;
			}

			return await _constantsService.CreateEditAsync(input);
		}

		[HttpDelete] // Delete Constant
		public async Task<OperationResult> Delete(int id)
		{
			return await _constantsService.DeleteAsync(id);
		}
	}
}
