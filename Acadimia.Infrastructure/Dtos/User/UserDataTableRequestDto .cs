using Acadimia.Data.Models;

namespace Acadimia.Infrastructure.Dtos.User
{
    public class UserDataTableRequestDto : DataTableRequestDto
    {
        public int? UserTypeId { get; set; }
        public int? GenderId { get; set; }
        public bool? IsActiveSearch { get; set; }
    }
}
