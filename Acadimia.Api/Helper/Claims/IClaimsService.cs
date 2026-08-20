using Acadimia.Data.Models;

namespace Acadimia.Api.Helper.Claims
{
    public interface IClaimsService
    {
        Task UpdateUserClaims(User user);
    }

}
