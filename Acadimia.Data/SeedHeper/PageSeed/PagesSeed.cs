using Acadimia.Data.Enums;
using Acadimia.Data.Models;

namespace Acadimia.Data.SeedHeper.PageSeed
{
    public static class PagesSeed
    {
        // Returns the desired pages (parents + tool pages). No HasData: PageSeeder inserts them idempotently.
        public static List<Page> GetPages()
        {
            var pages = new List<Page>
            {
                new() { Id = 1, Name = "الاب", NameEn = "Parent Page", CategoryId = (int)GeneralEnums.Header },
                new() { Id = 2, Name = "الرئيسية", NameEn = "Home", Icon = "bi bi-house-fill", Link = "Home/Index",
                        InMenu = true, ParentId = (int)GeneralEnums.ParentPageId, IsActive = true,
                        CategoryId = (int)GeneralEnums.Page },
                new() { Id = 3, Name = "الإدارة", NameEn = "Management", Icon = "bi bi-list-ul",
                        InMenu = true, ParentId = (int)GeneralEnums.ParentPageId, IsActive = true,
                        ModuleId = (int)GeneralEnums.Management, CategoryId = (int)GeneralEnums.Header },
                new() { Id = 4, Name = "إدارة المستخدمين", NameEn = "Users Management", Icon = "bi bi-people",
                        InMenu = true, ParentId = 3, IsActive = true,
                        ModuleId = (int)GeneralEnums.Management, CategoryId = (int)GeneralEnums.Header },
                Pg(5,  "المستخدمين",        "Users",                   "bi bi-person-fill",  "User/Index",           4),
                Pg(6,  "أنواع المستخدمين",  "User Types",              "bi bi-people",       "UserType/Index",       4),
                Pg(7,  "صلاحيات المستخدم",  "User Permissions",        "bi bi-check-lg",     "UserPermission/Index", 4),
                Pg(8,  "المحافظات و المدن", "Governorates and Cities", "bi bi-geo-alt-fill", "Destination/Index",    3),
                Pg(9,  "وحدات النظام",      "System Modules",          "bi bi-view-list",    "Management/Modules",   3),
                Pg(10, "الصفحات",           "Pages",                   "bi bi-window-stack", "Page/Index",           3),
                Pg(11, "الثوابت",           "Constants",               "fa fa-anchor",       "Constant/Index",       3),
            };

            pages.AddRange(ToolPagesSeed.AddToollPages(pages.Last().Id));
            return pages;
        }

        private static Page Pg(int id, string name, string nameEn, string icon, string link, int parentId) => new()
        {
            Id = id,
            Name = name,
            NameEn = nameEn,
            Icon = icon,
            Link = link,
            InMenu = true,
            ParentId = parentId,
            IsActive = true,
            ModuleId = (int)GeneralEnums.Management,
            CategoryId = (int)GeneralEnums.Page
        };
    }
}