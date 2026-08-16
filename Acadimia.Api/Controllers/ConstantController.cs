//using Acadimia.Data.Resources;
//using Acadimia.Infrastructure.Services.Constants;
//using Acadimia.Infrastructure.Services;
//using Microsoft.AspNetCore.Mvc;
//using Acadimia.Data.Models;
////using Acadimia.Api.ViewModel.Constants;

//namespace Acadimia.Web.Controllers
//{
//	public class ConstantController : BaseController
//	{
//		private readonly IConstantsService _constantsService;
//		public ConstantController(IConstantsService constantsService)
//		{
//			_constantsService = constantsService;
//		}

//		[HttpPost] // Display Constant DataTable
//        public async Task<IActionResult> GetAll()
//		{
//			var inputSearch = Request.Form["search[value]"];
//			var obj = !string.IsNullOrEmpty(inputSearch)
//				? JsonConvert.DeserializeObject<Constant>(inputSearch) : new Constant();
            
//            var result = await _constantsService.GetAllAsync(new PagedResultRequestDto<Constant>
//			{
//				SearchValue = obj,
//				SortColumn = Request.Form[string.Concat("columns[", Request.Form["order[0][column]"], "][name]")],
//				SortColumnDirection = Request.Form["order[0][dir]"],
//				PageSize = int.Parse(Request.Form["length"]),
//				Skip = int.Parse(Request.Form["start"])
//			});

//			return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
//		}

//		[HttpGet] // Display Constant Page
//        public async Task<IActionResult> Index()
//		{
//			return View(new IndexConstantVM
//			{
//				Parents = await _constantsService.GetParentsListItemAsync()
//            });
//		}

//		[HttpGet] // Display Create Edit Constant Page
//        public async Task<IActionResult> CreateEditModal(int id)
//		{            
//			return PartialView("_CreateEditModal", new CreateEditConstantVM
//            {
//                Constant = await _constantsService.GetByIdOrDefaultAsync(id),
//                Parents = await _constantsService.GetParentsListItemAsync(),
//            });
//		}

//		[HttpPost] // Create Edit Constant
//        public async Task<OperationResult> CreateEdit(Constant input)
//		{
//			var result = new OperationResult(false, Messages.Invalid);
//			if (!ModelState.IsValid)
//			{
//				var message = string.Join("<br>  ", ModelState.Values
//					.SelectMany(v => v.Errors)
//					.Select(e => e.ErrorMessage));
//				result.Message = message;
//				return result;
//			}

//			return await _constantsService.CreateEditAsync(input);
//		}

//		[HttpDelete] // Delete Constant
//        public async Task<OperationResult> Delete(int id)
//		{
//			return await _constantsService.DeleteAsync(id);
//		}
//	}
//}
