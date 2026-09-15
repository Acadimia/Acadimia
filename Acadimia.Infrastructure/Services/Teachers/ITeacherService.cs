using Acadimia.Core.Enums;
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

        // ---- Teacher Discovery & Booking additions ----
        Task<PagedResultDto<List<TeacherProfileDto>>> SearchAsync(TeacherSearchFilterDto filter);
        Task<TeacherProfileDto?> GetPublicProfileAsync(int teacherId);
        Task<OperationResult> UpdateProfileAsync(string userId, TeacherProfileInputDto input);
        Task<OperationResult> SetAvailabilityAsync(string userId, TeacherAvailabilityInputDto input);
        Task<List<TeacherAvailability>> GetAvailabilityAsync(int teacherId, CourseDeliveryType? mode, DateTime? date);
    }
}