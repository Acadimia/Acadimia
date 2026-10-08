using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class NationalitySeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            var existing = await context.Nationalities.ToDictionaryAsync(n => n.Id);
            var missing = new List<Nationality>();

            foreach (var n in NationalityData.All)
            {
                if (existing.TryGetValue(n.Id, out var row)) { row.NameAr = n.NameAr; row.NameEn = n.NameEn; }
                else missing.Add(n);
            }
            await context.SaveChangesAsync();

            if (missing.Count == 0) return;

            await context.Database.CreateExecutionStrategy().ExecuteAsync(async () =>
            {
                await using var tx = await context.Database.BeginTransactionAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Nationalities ON");
                context.Nationalities.AddRange(missing);
                await context.SaveChangesAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT Nationalities OFF");
                await tx.CommitAsync();
            });
        }
    }
}