using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Courses;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Courses;
using Acadimia.Infrastructure.Services.Ownership;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Acadimia.Api.Controllers
{
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService ;
        private readonly IOwnershipService _ownershipService;
        public CourseController(ICourseService courseService, Acadimia.Infrastructure.Services.Ownership.IOwnershipService ownershipService)
        {
            _courseService = courseService;
            _ownershipService = ownershipService;
        }

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

            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            var ownsTeacher = await _ownershipService.OwnsTeacherAsync(userId, input.TeacherId);
            var ownsExistingCourse = input.Id == 0 || await _ownershipService.OwnsCourseAsync(userId, input.Id);
            if (!ownsTeacher || !ownsExistingCourse) return new OperationResult(false, Messages.Failed);

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

            var userId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
            if (!await _ownershipService.OwnsGroupAsync(userId, input.GroupId))
                return new OperationResult(false, Messages.Failed);

            return await _courseService.ConfigureGroupScheduleAsync(input);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupStudents(int groupId, string? keyword = null)
            => Ok(await _courseService.GetGroupStudentsAsync(groupId, keyword));
    }
}