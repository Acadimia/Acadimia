using Acadimia.Api.Helper.Authorization;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Courses;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Courses;
using Acadimia.Infrastructure.Services.Ownership;
using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> GetPublished([FromBody] DataTableRequestDto? request = null)
    {
        request ??= new DataTableRequestDto();
        var result = await _courseService.GetAllAsync(new PagedResultRequestDto<Course>
        {
            SearchValue = new Course { Status = CourseStatus.Published },
            SortColumn = request.SortColumn,
            SortColumnDirection = request.SortColumnDirection,
            PageSize = Math.Clamp(request.PageSize, 1, 50),
            Skip = Math.Max(request.Skip, 0)
        });
        return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
    }

    // محمي: الأدمن يشوف كل شي، المعلم كورساته فقط (كل الحالات)
    [HttpPost]
    [RequireUserTypes(UserTypeIds.Admin, UserTypeIds.Teacher)]
    public async Task<IActionResult> GetAll([FromBody] DataTableRequestDto? request = null)
    {
        request ??= new DataTableRequestDto();
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var teacherId = 0; // 0 = الكل
        if (await _ownershipService.GetUserTypeIdAsync(userId) != UserTypeIds.Admin)
            teacherId = await _ownershipService.GetTeacherIdForUserAsync(userId) ?? -1; // -1 = لا شي

        var result = await _courseService.GetAllAsync(new PagedResultRequestDto<Course>
        {
            SearchValue = new Course { TeacherId = teacherId },
            SortColumn = request.SortColumn,
            SortColumnDirection = request.SortColumnDirection,
            PageSize = Math.Clamp(request.PageSize, 1, 100),
            Skip = Math.Max(request.Skip, 0)
        });
        return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id)
    {
        var course = await _courseService.GetByIdAsync(id, publishedOnly: true);
        return course == null ? NotFound() : Ok(course);
    }

    // للمعلم/الأدمن: يشوف كورسه حتى لو Draft
    [HttpGet]
    [RequireUserTypes(UserTypeIds.Admin, UserTypeIds.Teacher)]
    public async Task<IActionResult> GetMineById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = await _ownershipService.GetUserTypeIdAsync(userId) == UserTypeIds.Admin;
        if (!isAdmin && !await _ownershipService.OwnsCourseAsync(userId, id)) return StatusCode(403);

        var course = await _courseService.GetByIdAsync(id, publishedOnly: false);
        return course == null ? NotFound() : Ok(course);
    }

    [HttpGet]
    [RequireUserTypes(UserTypeIds.Admin, UserTypeIds.Teacher)]
    public async Task<IActionResult> GetGroupStudents(int groupId, string? keyword = null)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var isAdmin = await _ownershipService.GetUserTypeIdAsync(userId) == UserTypeIds.Admin;
        if (!isAdmin && !await _ownershipService.OwnsGroupAsync(userId, groupId)) return StatusCode(403);

        return Ok(await _courseService.GetGroupStudentsAsync(groupId, keyword));
    }

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

       
    }
}