using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.SeedHeper
{
    public static class UserSeed
    {
        public static void Seed(this ModelBuilder builder, List<Page> Pages)
        {
            // seed User Type
            builder.Entity<UserType>().HasData(
                new UserType { Id = 1, Name = "مدير النظام" },
                new UserType { Id = 2, Name = "مستخدم" }
            );

            var adminUserId = "D3E20CBB-2AD1-4D55-9A1E-4CEEC5B4CDE3";

            // create admin user
            var adminUser = new User
            {
                Id = adminUserId,
                Name = "Academia Admin",
                Email = "admin@Academia.com",
                UserName = "admin@Academia.com",
                PhoneNumber = "",
                NormalizedUserName = "ADMIN@Academia.COM",
                UserTypeId = 1, // Assuming 1 is the ID for General Manager
                GenderId = (int)GeneralEnums.Male,
                IsActive = true,
                Avatar = "default_avatar.png"
            };

            // set user password hash 
            adminUser.PasswordHash = "0594727849Ziad#";
            // seed user
            builder.Entity<User>().HasData(adminUser);

            // seed UserPermissions for admin user type
            var userPermissions = new List<UserPermission>();

            int userPermissionId = 1; // Start ID for UserPermissions
            foreach (var page in Pages)
            {
                userPermissions.Add(new UserPermission
                {
                    Id = userPermissionId++,
                    UserTypeId = 1, // General Manager
                    PageId = page.Id
                });
            }

            builder.Entity<UserPermission>().HasData(userPermissions);
        }
    }
}
