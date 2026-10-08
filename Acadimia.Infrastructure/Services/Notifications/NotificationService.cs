using Acadimia.Data.Enums;
using Acadimia.Data.DbContext;
using Acadimia.Data.Models;

namespace Acadimia.Infrastructure.Services.Notifications
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        public NotificationService(ApplicationDbContext context) => _context = context;

        public async Task CreateAsync(string userId, string title, string message, NotificationType type,
            string? relatedEntityType = null, int? relatedEntityId = null)
        {
            await _context.Notifications.AddAsync(new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = type,
                IsRead = false,
                RelatedEntityType = relatedEntityType,
                RelatedEntityId = relatedEntityId,
                CreatedOn = DateTime.Now
            });
            await _context.SaveChangesAsync();
        }
        public async Task CreateManyAsync(IEnumerable<string> userIds, string title, string message, NotificationType type,
                string? relatedEntityType = null, int? relatedEntityId = null)
        {
            var now = DateTime.Now;
            var items = userIds.Distinct().Select(uid => new Notification
            {
                UserId = uid,
                Title = title,
                Message = message,
                Type = type,
                IsRead = false,
                RelatedEntityType = relatedEntityType,
                RelatedEntityId = relatedEntityId,
                CreatedOn = now
            }).ToList();

            if (items.Count == 0) return;
            await _context.Notifications.AddRangeAsync(items);
            await _context.SaveChangesAsync();
        }

    }
}