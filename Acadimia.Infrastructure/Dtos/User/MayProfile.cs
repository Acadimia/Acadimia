using Acadimia.Data.Models;
using Acadimia.Infrastructure.Services.Users.Dto;

namespace Acadimia.Infrastructure.Dtos.User
{
    public class MyProfile
    {
        public MyProfileDto MyProfileDto { get; set; }
        public List<Constant> Genders { get; set; }
    }
}
