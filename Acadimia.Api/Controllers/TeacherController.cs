using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos;
using Acadimia.Infrastructure.Dtos.Teachers;
using Acadimia.Infrastructure.Services;
using Acadimia.Infrastructure.Services.Teachers;
using Microsoft.AspNetCore.Mvc;

namespace Acadimia.Api.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        [HttpPost]
        public async Task<IActionResult> GetAll([FromBody] TeacherDataTableRequestDto? request = null)
        {
            request ??= new TeacherDataTableRequestDto();

            var filter = new Teacher { GradeId = request.GradeId ?? 0 };

            var result = await _teacherService.GetAllAsync(new PagedResultRequestDto<Teacher>
            {
                SearchValue = filter,
                SortColumn = request.SortColumn,
                SortColumnDirection = request.SortColumnDirection,
                PageSize = request.PageSize,
                Skip = request.Skip
            });

            return Ok(new { recordsFiltered = result.TotalCount, result.TotalCount, result.Data });
        }

        [HttpGet]
        public async Task<IActionResult> CreateEditModal(int id)
        {
            return Ok(new
            {
                Teacher = await _teacherService.GetByIdOrDefaultAsync(id),
                Grades = await _teacherService.GetGradesListAsync()
            });
        }

        [HttpPost]
        public async Task<OperationResult> CreateEdit(TeacherInputDto input)
        {
            var result = new OperationResult(false, "Invalid");
            if (!ModelState.IsValid)
            {
                result.Message = string.Join("<br>", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                return result;
            }

            return await _teacherService.CreateEditAsync(input);
        }

        [HttpDelete]
        public async Task<OperationResult> Delete(int id)
        {
            return await _teacherService.DeleteAsync(id);
        }
    }
}