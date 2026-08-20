using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Pages;
//using Acadimia.Web.ViewModel.Pages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Acadimia.Web.Controllers
{
    public class PageController : BaseController
    {
        private readonly IPagesService _pagesService;
        public PageController(IPagesService pagesService)
        {
            _pagesService = pagesService;
        }

        [HttpPost] // Display Pages DataTable
        public async Task<IActionResult> GetAll()
        {
            var inputSearch = Request.Form["search[value]"];
            var obj = !string.IsNullOrEmpty(inputSearch)
                ? JsonConvert.DeserializeObject<Page>(inputSearch) : new Page();

            var result = await _pagesService.GetAllAsync(new PagedResultRequestDto<Page>
            {
                SearchValue = obj,
                SortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")],
                SortColumnDirection = Request.Form["order[0][dir]"],
                PageSize = int.Parse(Request.Form["length"]),
                Skip = int.Parse(Request.Form["start"])
            });

            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        //[HttpGet]  Display Pages page
        //public async Task<IActionResult> Index()
        //{
        //    List<Page> parents = await _pagesService.GetParentsListAsync();
        //    var parentsItem = parents.Select(p => new SelectListItem
        //    {
        //        Value = p.Id.ToString(),
        //        Text = p.Name
        //    }).ToList();

        //    return View(new IndexPageVM
        //    {
        //        Parents = parentsItem,
        //        Modules = await _pagesService.GetModulesListAsync()
        //    });
        //}

        //[HttpGet] // Display Create Edit Page interface
        //public async Task<IActionResult> CreateEditModal(int id)
        //{
        //    List<Page> parents = await _pagesService.GetParentsListAsync();
        //    var parentsItem = parents.Select(p => new SelectListItem
        //    {
        //        Value = p.Id.ToString(),
        //        Text = p.Name
        //    }).ToList();

        //    var pageVM = new CreateEditPageVM
        //    {
        //        Page = await _pagesService.GetByIdOrDefaultAsync(id),
        //        Modules = await _pagesService.GetModulesListAsync(),
        //        Parents = parentsItem,
        //        Categories = await _pagesService.GetCategoriesListAsync()

        //    };

        //    return PartialView("_CreateEditModal", pageVM);
        //}

        [HttpPost] // Create Edit Page
        public async Task<OperationResult> CreateEdit(Page input)
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

            return await _pagesService.CreateEditAsync(input);
        }

        [HttpDelete] // Delete Page
        public async Task<OperationResult> Delete(int id)
        {
            return await _pagesService.DeleteAsync(id);
        }
    }
}
