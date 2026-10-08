using Acadimia.Data.DbContext;
using Acadimia.Data.SeedHeper.PageSeed;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class PageSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var desired = PagesSeed.GetPages();
            var existingIds = (await context.Pages.IgnoreQueryFilters().Select(p => p.Id).ToListAsync()).ToHashSet();

            // Parents first: parents always have lower Ids here.
            foreach (var p in desired.OrderBy(x => x.Id))
            {
                // Never overwrite edits made by the admin through the Pages screen.
                if (existingIds.Contains(p.Id)) continue;

                var page = p;
                await context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
                {
                    await using var tx = await context.Database.BeginTransactionAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Pages ON");
                    context.Pages.Add(page);
                    await context.SaveChangesAsync();
                    await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Pages OFF");
                    await tx.CommitAsync();
                });
                context.ChangeTracker.Clear();
            }
        }
    }
}