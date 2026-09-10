using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Lessons;

namespace Acadimia.Infrastructure.Services.Lessons
{
    public interface ILessonService
    {
        Task<OperationResult> CreateAsync(LessonInputDto input);  
        Task<OperationResult> UpdateAsync(LessonInputDto input);   
        Task<OperationResult> CancelAsync(LessonCancelDto input); 
        Task<OperationResult> ConfigureMeetingAsync(MeetingConfigInputDto input); 
        Task<List<Lesson>> GetScheduleAsync(int? courseId, int? groupId); 
    }
}