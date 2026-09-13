using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Courses;

namespace Acadimia.Infrastructure.Services.Courses
{
    public interface ICourseService
    {
        Task<PagedResultDto<List<Course>>> GetAllAsync(PagedResultRequestDto<Course> input);
        Task<Course> GetByIdOrDefaultAsync(int id);
        Task<OperationResult> CreateEditAsync(CourseInputDto input); 
        Task<OperationResult> ConfigureGroupScheduleAsync(GroupScheduleInputDto input); 
        Task<List<StudentGroupRosterDto>> GetGroupStudentsAsync(int groupId, string? keyword);

    }
}