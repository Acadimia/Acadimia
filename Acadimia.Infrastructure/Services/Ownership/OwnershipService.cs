using Acadimia.Data.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Infrastructure.Services.Ownership
{
    public class OwnershipService : IOwnershipService
    {
        private readonly ApplicationDbContext _context;
        public OwnershipService(ApplicationDbContext context) => _context = context;

        public async Task<int?> GetTeacherIdForUserAsync(string userId) =>
            await _context.Teachers.Where(t => t.UserId == userId).Select(t => (int?)t.Id).FirstOrDefaultAsync();

        public async Task<bool> OwnsTeacherAsync(string userId, int teacherId) =>
            await _context.Teachers.AnyAsync(t => t.Id == teacherId && t.UserId == userId);

        public async Task<bool> OwnsCourseAsync(string userId, int courseId) =>
            await _context.Courses.AnyAsync(c => c.Id == courseId && c.Teacher.UserId == userId);

        public async Task<bool> OwnsGroupAsync(string userId, int groupId) =>
            await _context.Groups.AnyAsync(g => g.Id == groupId && g.Teacher.UserId == userId);

        public async Task<bool> OwnsLessonAsync(string userId, int lessonId) =>
            await _context.Lessons.Where(l => l.Id == lessonId).AnyAsync(l =>
                (l.Group != null && l.Group.Teacher.UserId == userId) ||
                (l.Course != null && l.Course.Teacher.UserId == userId));

        public async Task<int?> GetUserTypeIdAsync(string userId) =>
    await _context.Users.Where(u => u.Id == userId && !u.IsDeleted && u.IsActive)
        .Select(u => (int?)u.UserTypeId).FirstOrDefaultAsync();
    }
}