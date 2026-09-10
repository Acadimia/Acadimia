using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Courses;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Courses;
using Microsoft.AspNetCore.Mvc;

namespace Acadimia.Api.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService) => _courseService = courseService;

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto? request = null)
        {
            request ??= new DataTableRequestDto();
            var result = await _courseService.GetAllAsync(new PagedResultRequestDto<Course>
            {
                SearchValue = new Course(),
                SortColumn = request.SortColumn,
                SortColumnDirection = request.SortColumnDirection,
                PageSize = request.PageSize,
                Skip = request.Skip
            });
            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> GetById(int id) => Ok(await _courseService.GetByIdOrDefaultAsync(id));

        [HttpPost]
        public async Task<OperationResult> CreateEdit(CourseInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return result;
            }
            return await _courseService.CreateEditAsync(input);
        }

        [HttpPost]
        public async Task<OperationResult> ConfigureGroupSchedule(GroupScheduleInputDto input)
        {
            var result = new OperationResult(false, Messages.Invalid);
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return result;
            }
            return await _courseService.ConfigureGroupScheduleAsync(input);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupStudents(int groupId, string? keyword = null)
            => Ok(await _courseService.GetGroupStudentsAsync(groupId, keyword));
    }
}