using Acadimia.Data.DbContext;
using Acadimia.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class UserTypeSeeder
    {
        // Ids come from UserTypeIds (the code is the source of truth).
        private static readonly (int Id, string Name)[] Types =
        {
            (UserTypeIds.Admin,   "مدير النظام"),
            (UserTypeIds.Student, "الطالب"),
            (UserTypeIds.Teacher, "المعلم"),
            (UserTypeIds.Parent,  "ولي الامر"),
        };

        public static async Task SeedAsync(ApplicationDbContext context)
        {
            // IgnoreQueryFilters: a soft-deleted row must not be re-inserted with the same Id.
            var existing = await context.UserTypes.IgnoreQueryFilters().ToListAsync();

            foreach (var (id, name) in Types)
            {
                var row = existing.FirstOrDefault(x => x.Id == id);
                if (row == null)
                {
                    await InsertWithIdentityAsync(context, id, name);
                }
                else if (row.Name != name || row.IsDeleted)
                {
                    // Correct drifted names (old migration had 2="مستخدم", 3="الطالب", ...).
                    // If the unique-name index complains, run the two-step SQL fix first.
                    row.Name = name;
                    row.IsDeleted = false;
                }
            }
            await context.SaveChangesAsync();
        }

        private static async Task InsertWithIdentityAsync(ApplicationDbContext context, int id, string name)
        {
            await context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
            {
                await using var tx = await context.Database.BeginTransactionAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT UserTypes ON");
                await context.Database.ExecuteSqlInterpolatedAsync(
                    $"INSERT INTO UserTypes (Id, Name, IsDeleted, CreatedOn) VALUES ({id}, {name}, 0, {DateTime.Now})");
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT UserTypes OFF");
                await tx.CommitAsync();
            });
        }
    }
}