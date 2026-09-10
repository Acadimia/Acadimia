using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Teachers;

namespace Acadimia.Infrastructure.Services.Teachers
{
    public interface ITeacherService
    {
        Task<PagedResultDto<List<Teacher>>> GetAllAsync(PagedResultRequestDto<Teacher> input);
        Task<Teacher> GetByIdOrDefaultAsync(int id);
        Task<OperationResult> CreateEditAsync(TeacherInputDto input);
        Task<OperationResult> DeleteAsync(int id);
        Task<List<Grade>> GetGradesListAsync();
    }
}