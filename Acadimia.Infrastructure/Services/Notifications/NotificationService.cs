using Acadimia.Core.Enums;
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
    }
}