using Acadimia.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Acadimia.Api.Helper.Claims
{
    public class ClaimsService : IClaimsService
    {
        private readonly UserManager<User> _userManager;

        public ClaimsService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task UpdateUserClaims(User user)
        {
            var currentClaims = await _userManager.GetClaimsAsync(user);
            if (currentClaims.Any())
            {
                await _userManager.RemoveClaimsAsync(user, currentClaims);
            }

            var newClaims = new List<Claim>
                {
                    new Claim("Name", user.Name),
                    new Claim("Avatar", user.Avatar ?? "default_avatar.png")
                };

            await _userManager.AddClaimsAsync(user, newClaims);
        }
    }
}
