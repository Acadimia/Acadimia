using Acadimia.Data.Models;

namespace Acadimia.Web.Helper.Claims
{
    public interface IClaimsService
    {
        Task UpdateUserClaims(User user);
    }

}
