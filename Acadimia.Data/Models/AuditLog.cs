namespace Acadimia.Data.Models
{
    // Cross-cutting change log supporting the audit-trail non-functional
    // requirements, especially for wallet and permission changes.
    public class AuditLog : BaseModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public string Action { get; set; }
        public string EntityName { get; set; }
        public string EntityId { get; set; }

        public string? OldValues { get; set; }
        public string? NewValues { get; set; }
    }
}
