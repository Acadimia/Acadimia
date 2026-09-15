namespace Acadimia.Infrastructure.Services.Ownership
{
    public interface IOwnershipService
    {
        Task<int?> GetTeacherIdForUserAsync(string userId);
        Task<bool> OwnsTeacherAsync(string userId, int teacherId);
        Task<bool> OwnsCourseAsync(string userId, int courseId);
        Task<bool> OwnsGroupAsync(string userId, int groupId);
        Task<bool> OwnsLessonAsync(string userId, int lessonId);
    }
}