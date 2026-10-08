using Acadimia.Data.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Acadimia.Data.SeedHeper
{
    public static class DatabaseSeeder
    {
        // Execution order matters: parents before children.
        public static async Task SeedAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var sp = scope.ServiceProvider;
            var context = sp.GetRequiredService<ApplicationDbContext>();
            var logger = sp.GetRequiredService<ILoggerFactory>().CreateLogger("DatabaseSeeder");

            await context.Database.MigrateAsync();

            await UserTypeSeeder.SeedAsync(context);        // 1
            await PageSeeder.SeedAsync(context);            // 2 (needs Modules + PageCategories from HasData)
            await PermissionSeeder.SeedAsync(context);      // 3 (needs Pages + UserTypes)
            await AdminUserSeeder.SeedAsync(sp);            // 4 (needs UserTypes + Constants)
            await AcademicLookupSeeder.SeedAsync(context);  // 5 Grades, Subjects, CourseCategories
            await CommissionSeeder.SeedAsync(context);      // 6
            await NationalitySeeder.SeedAsync(context);     // 7

            logger.LogInformation("Database seeding completed.");
        }
    }
}