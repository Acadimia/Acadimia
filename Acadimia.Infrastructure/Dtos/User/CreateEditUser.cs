using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Users.Dto;

namespace Acadimia.Infrastructure.Dtos.User
{
    public class CreateEditUser
    {
        public UserDto User { get; set; }
        public List<UserType> UserTypes { get; set; }
        public List<Constant> Genders { get; set; }
    }
}
