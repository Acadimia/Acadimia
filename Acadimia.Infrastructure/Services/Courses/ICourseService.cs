using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Courses;

namespace Acadimia.Infrastructure.Services.Courses
{
    public interface ICourseService
    {
        Task<PagedResultDto<List<Course>>> GetAllAsync(PagedResultRequestDto<Course> input);
        Task<Course> GetByIdOrDefaultAsync(int id);
        Task<OperationResult> CreateEditAsync(CourseInputDto input); // FR-I01 / FR-I05
        Task<OperationResult> ConfigureGroupScheduleAsync(GroupScheduleInputDto input); // FR-I02 / FR-I06
        Task<List<Student>> GetGroupStudentsAsync(int groupId, string? keyword); // FR-I03
    }
}