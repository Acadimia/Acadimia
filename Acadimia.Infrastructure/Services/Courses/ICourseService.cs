using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Courses;

namespace Acadimia.Infrastructure.Services.Courses
{
    public interface ICourseService
    {
        Task<PagedResultDto<List<CourseListItemDto>>> GetAllAsync(PagedResultRequestDto<Course> input);
        Task<CourseListItemDto?> GetByIdAsync(int id, bool publishedOnly);
        Task<OperationResult> CreateEditAsync(CourseInputDto input); 
        Task<OperationResult> ConfigureGroupScheduleAsync(GroupScheduleInputDto input); 
        Task<List<StudentGroupRosterDto>> GetGroupStudentsAsync(int groupId, string? keyword);

    }
}