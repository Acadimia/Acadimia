using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Pages;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Pages;
using Microsoft.AspNetCore.Mvc;

namespace Acadimia.Api.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPagesService _pagesService;
        public PageController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto? request = null)
        {
            request ??= new DataTableRequestDto();

            var filter = new Page { Keyword = request.SearchValue };

            var result = await _pagesService.GetAllAsync(new PagedResultRequestDto<Page>
            {
                SearchValue = filter,
                SortColumn = request.SortColumn,
                SortColumnDirection = request.SortColumnDirection,
                PageSize = request.PageSize,
                Skip = request.Skip
            });

            return Ok(new
            {
                recordsFiltered = result.TotalCount,
                recordsTotal = result.TotalCount,
                data = result.Data
            });
        }

        [HttpGet] // Display Create Edit Page data
        public async Task<IActionResult> CreateEditModal(int id)
        {
            List<Page> parents = await _pagesService.GetParentsListAsync();

            return Ok(new
            {
                Page = await _pagesService.GetByIdOrDefaultAsync(id),
                Modules = await _pagesService.GetModulesListAsync(),
                Categories = await _pagesService.GetCategoriesListAsync(),
                Parents = parents.Select(p => new { p.Id, p.Name })
            });
        }

        [HttpPost] // Create Edit Page
        public async Task<OperationResult> CreateEdit(PageInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>  ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return result;
            }

            var entity = new Page
            {
                Id = input.Id,
                Name = input.Name,
                NameEn = input.NameEn,
                Link = input.Link,
                Icon = input.Icon,
                InMenu = input.InMenu,
                ParentId = input.ParentId,
                IsActive = input.IsActive,
                IsAjax = input.IsAjax,
                ModuleId = input.ModuleId,
                CategoryId = input.CategoryId
            };

            return await _pagesService.CreateEditAsync(entity);
        }

        [HttpDelete] // Delete Page
        public async Task<OperationResult> Delete(int id)
        {
            return await _pagesService.DeleteAsync(id);
        }
    }
}