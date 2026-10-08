using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Acadimia.Data.SeedHeper
{
    public static class SeedHelper
    {
        // Static reference data only: no timestamps, no Identity, safe for HasData.
        // Everything else (UserTypes, Pages, Permissions, Admin, lookups, Nationalities)
        // is seeded at runtime by DatabaseSeeder.
        public static void SeedStaticReferenceData(this ModelBuilder builder)
        {
            SeedPageCategories(builder);
            SeedModules(builder);
            SeedConstants(builder);
        }

        private static void SeedPageCategories(ModelBuilder builder)
        {
            builder.Entity<PageCategory>().HasData(
                new PageCategory { Id = 1, Name = "Header" },
                new PageCategory { Id = 2, Name = "Page" },
                new PageCategory { Id = 3, Name = "Tool" }
            );
        }

        private static void SeedModules(ModelBuilder builder)
        {
            builder.Entity<Module>().HasData(
                new Module { Id = 1, Name = "الادارة", Status = true },
                new Module { Id = 2, Name = "إدارة العملاء", Status = true },
                new Module { Id = 3, Name = "إدارة الخدمات", Status = true },
                new Module { Id = 4, Name = "المالية", Status = true },
                new Module { Id = 5, Name = "البريد", Status = false },
                new Module { Id = 6, Name = "المصروفات", Status = false },
                new Module { Id = 7, Name = "الخدمات", Status = false },
                new Module { Id = 8, Name = "التقارير", Status = false }
            );
        }

        private static void SeedConstants(ModelBuilder builder)
        {
            builder.Entity<Constant>().HasData(
                new Constant { Id = 1, Name = "العملة" },
                new Constant { Id = 2, Name = "دولار", ParentId = 1 },
                new Constant { Id = 3, Name = "دينار", ParentId = 1 },
                new Constant { Id = 4, Name = "شيكل", ParentId = 1 },

                new Constant { Id = 5, Name = "الجنس" },
                new Constant { Id = 6, Name = "ذكر", ParentId = 5 },
                new Constant { Id = 7, Name = "أنثى", ParentId = 5 },

                new Constant { Id = 8, Name = "نوع المكان المقصود" },
                new Constant { Id = 9, Name = "دولة", ParentId = 8 },
                new Constant { Id = 10, Name = "مدينة", ParentId = 8 },
                new Constant { Id = 11, Name = "محافظة", ParentId = 8 },

                new Constant { Id = 12, Name = "نوع المرفق" },
                new Constant { Id = 13, Name = "جواز سفر", ParentId = 12 },
                new Constant { Id = 14, Name = "هوية", ParentId = 12 },
                new Constant { Id = 15, Name = "شهادة ثانوية عامة", ParentId = 12 },
                new Constant { Id = 16, Name = "شهادة دبلوم", ParentId = 12 },
                new Constant { Id = 17, Name = "شهادة بكالوريس", ParentId = 12 },
                new Constant { Id = 18, Name = "شهادة ماجستير", ParentId = 12 },

                new Constant { Id = 20, Name = "صلة القرابة" },
                new Constant { Id = 21, Name = "اب", ParentId = 20 },
                new Constant { Id = 22, Name = "ام", ParentId = 20 },
                new Constant { Id = 23, Name = "ابن", ParentId = 20 },
                new Constant { Id = 24, Name = "بنت", ParentId = 20 },
                new Constant { Id = 25, Name = "زوج", ParentId = 20 },
                new Constant { Id = 26, Name = "زوجة", ParentId = 20 },
                new Constant { Id = 27, Name = "وصي", ParentId = 20 }
            );
        }
    }
}