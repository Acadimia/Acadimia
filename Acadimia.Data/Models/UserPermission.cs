namespace Acadimia.Data.Models
{
    public class UserPermission
    {
        public int Id { get; set; }
        public int UserTypeId { get; set; }
        public UserType? UserType { get; set; }
        public int PageId { get; set; }
        public Page? Page { get; set; }
    }
}