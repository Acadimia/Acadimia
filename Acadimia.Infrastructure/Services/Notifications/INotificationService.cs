using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Services.Notifications
{
    public interface INotificationService
    {
        Task CreateAsync(string userId, string title, string message, NotificationType type,
            string? relatedEntityType = null, int? relatedEntityId = null);
    }
}