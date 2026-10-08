using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class CommissionSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (await context.PlatformCommissionSettings.IgnoreQueryFilters().AnyAsync()) return;

            context.PlatformCommissionSettings.Add(new PlatformCommissionSetting
            {
                CommissionPercentage = 10m,   // matches BookingService.DefaultCommissionPercentage
                EffectiveFrom = DateTime.Today,
                IsActive = true
            });
            await context.SaveChangesAsync();
        }
    }
}