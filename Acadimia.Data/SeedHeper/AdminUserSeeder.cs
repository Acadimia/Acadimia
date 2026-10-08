using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Acadimia.Data.SeedHeper
{
    public static class AdminUserSeeder
    {
        public static async Task SeedAsync(IServiceProvider sp)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var config = sp.GetRequiredService<IConfiguration>();

            var email = config["Seed:AdminEmail"] ?? "admin@Academia.com";
            var password = config["Seed:AdminPassword"];

            if (await userManager.FindByEmailAsync(email) != null) return;

            if (string.IsNullOrWhiteSpace(password))
                throw new InvalidOperationException(
                    "Seed:AdminPassword is not configured. Set it via user-secrets or an environment variable (Seed__AdminPassword).");

            var admin = new User
            {
                Id = "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3",
                Name = "Academia Admin",
                Email = email,
                UserName = email,
                PhoneNumber = "0000000000",
                GenderId = (int)GeneralEnums.Male,
                UserTypeId = UserTypeIds.Admin,
                IsActive = true,
                EmailConfirmed = true,
                Avatar = "default_avatar.png"
            };

            var result = await userManager.CreateAsync(admin, password);
            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
        }
    }
}