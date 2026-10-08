using Acadimia.Data.DbContext;
using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class AcademicLookupSeeder
    {
        // Seeds only when a table is empty so admin-managed data is never touched.
        // The names below are assumptions: adjust to your real curriculum.
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            if (!await context.CourseCategories.IgnoreQueryFilters().AnyAsync())
                context.CourseCategories.AddRange(
                    new CourseCategory { Name = "تحضير للامتحانات" },
                    new CourseCategory { Name = "إثراء" },
                    new CourseCategory { Name = "دروس خصوصية" });

            if (!await context.Subjects.IgnoreQueryFilters().AnyAsync())
                context.Subjects.AddRange(
                    new Subject { Name = "الرياضيات" },
                    new Subject { Name = "الفيزياء" },
                    new Subject { Name = "الكيمياء" },
                    new Subject { Name = "الأحياء" },
                    new Subject { Name = "اللغة العربية" },
                    new Subject { Name = "اللغة الإنجليزية" });

            if (!await context.Grades.IgnoreQueryFilters().AnyAsync())
                context.Grades.AddRange(Enumerable.Range(1, 12)
                    .Select(i => new Grade { Name = $"الصف {i}", Section = "أ" }));

            await context.SaveChangesAsync();
        }
    }
}