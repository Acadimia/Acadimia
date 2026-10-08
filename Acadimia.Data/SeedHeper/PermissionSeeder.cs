using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class PermissionSeeder
    {
        // The admin type always gets every page; other types are managed from the permissions screen.
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var allPageIds = await context.Pages.IgnoreQueryFilters().Select(p => p.Id).ToListAsync();
            var have = (await context.UserPermissions
                .Where(up => up.UserTypeId == UserTypeIds.Admin)
                .Select(up => up.PageId).ToListAsync()).ToHashSet();

            var missing = allPageIds.Where(id => !have.Contains(id))
                .Select(id => new UserPermission { UserTypeId = UserTypeIds.Admin, PageId = id })
                .ToList();

            if (missing.Count == 0) return;

            await context.UserPermissions.AddRangeAsync(missing);
            await context.SaveChangesAsync();
        }
    }
}