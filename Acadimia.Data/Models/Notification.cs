using Acadimia.Data.Enums;

namespace Acadimia.Data.Models
{
    // In-app/dashboard and email alert for a user.
    public class Notification : BaseModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public bool IsRead { get; set; }

        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }
    }
}
