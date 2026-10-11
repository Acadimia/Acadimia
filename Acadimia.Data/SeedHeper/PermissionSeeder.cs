using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.SeedHeper.PageSeed;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class PermissionSeeder
    {
        private static readonly int[] RoleTypes =
        {
            UserTypeIds.Student, UserTypeIds.Teacher, UserTypeIds.Parent
        };

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await SeedAdminAsync(context);
            await SeedRolesAsync(context);
        }

        // الأدمن دائماً يحصل على كل الصفحات، ما عدا الصفحات الخاصة بالطالب/المعلم/ولي الأمر
        private static async Task SeedAdminAsync(ApplicationDbContext context)
        {
            var allPageIds = await context.Pages.IgnoreQueryFilters().Select(p => p.Id).ToListAsync();
            var have = (await context.UserPermissions
                .Where(up => up.UserTypeId == UserTypeIds.Admin)
                .Select(up => up.PageId).ToListAsync()).ToHashSet();

            var missing = allPageIds
                .Where(id => !have.Contains(id) && !RolePagesSeed.NonAdminPageIds.Contains(id))
                .Select(id => new UserPermission { UserTypeId = UserTypeIds.Admin, PageId = id })
                .ToList();

            if (missing.Count == 0) return;

            await context.UserPermissions.AddRangeAsync(missing);
            await context.SaveChangesAsync();
        }

        // صلاحيات افتراضية للطالب/المعلم/ولي الأمر.
        // تُزرع فقط إذا النوع ما عنده أي صلاحية، حتى لا نلغي تعديلات الأدمن من شاشة الصلاحيات.
        private static async Task SeedRolesAsync(ApplicationDbContext context)
        {
            var existingPageIds = (await context.Pages.IgnoreQueryFilters()
                .Select(p => p.Id).ToListAsync()).ToHashSet();

            foreach (var typeId in RoleTypes)
            {
                if (await context.UserPermissions.AnyAsync(up => up.UserTypeId == typeId))
                    continue;

                var perms = RolePagesSeed.DefaultPageIdsFor(typeId)
                    .Where(existingPageIds.Contains)
                    .Select(pageId => new UserPermission { UserTypeId = typeId, PageId = pageId })
                    .ToList();

                if (perms.Count == 0) continue;

                await context.UserPermissions.AddRangeAsync(perms);
            }

            await context.SaveChangesAsync();
        }
    }
}
