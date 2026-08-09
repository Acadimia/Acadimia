using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.SeedHeper.PageSeed
{
    public static class PagesSeed
    {
        // seed Pages and Pass Pages To User To Seed User Permissions
        public static void Seed(this ModelBuilder builder)
        {
            var pages = new List<Page>()
            {
                new Page()
                {
                    Id = 1,
                    Name = "الاب",
                    NameEn = "Parent Page",
                    CategoryId = (int)GeneralEnums.Header
                },
                new Page()
                {
                    Id = 2,
                    Name = "الرئيسية",
                    NameEn = "Home",
                    Icon = "bi bi-house-fill",
                    Link ="Home/Index",
                    InMenu = true,
                    ParentId = (int)GeneralEnums.ParentPageId,
                    IsActive = true,
                    ModuleId = null,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 3,
                    Name = "الإدارة",
                    NameEn = "Management",
                    Icon = "bi bi-list-ul",
                    Link = null,
                    InMenu = true,
                    ParentId = (int)GeneralEnums.ParentPageId,
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Header
                },
                new Page()
                {
                    Id = 4,
                    Name = "إدارة المستخدمين",
                    NameEn = "Users Management",
                    Icon = "bi bi-people",
                    Link = null,
                    InMenu = true,
                    ParentId = 3, // Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Header
                },
                new Page()
                {
                    Id = 5,
                    Name = "المستخدمين",
                    NameEn = "Users",
                    Icon = "bi bi-person-fill",
                    Link = "User/Index",
                    InMenu = true,
                    ParentId = 4, //Users Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 6,
                    Name = "أنواع المستخدمين",
                    NameEn = "User Types",
                    Icon = "bi bi-people",
                    Link = "UserType/Index",
                    InMenu = true,
                    ParentId = 4, //Users Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 7,
                    Name = "صلاحيات المستخدم",
                    NameEn = "User Permissions",
                    Icon = "bi bi-check-lg",
                    Link = "UserPermission/Index",
                    InMenu = true,
                    ParentId = 4, //Users Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 8,
                    Name = "المحافظات و المدن",
                    NameEn = "Governorates and Cities",
                    Icon = "bi bi-geo-alt-fill",
                    Link = "Destination/Index",
                    InMenu = true,
                    ParentId = 3, //Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 9,
                    Name = "وحدات النظام",
                    NameEn = "Governorates and Cities",
                    Icon = "bi bi-view-list",
                    Link = "Management/Modules",
                    InMenu = true,
                    ParentId = 3, //Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 10,
                    Name = "الصفحات",
                    NameEn = "Pages",
                    Icon = "bi bi-window-stack",
                    Link = "Page/Index",
                    InMenu = true,
                    ParentId = 3, //Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                },
                new Page()
                {
                    Id = 11,
                    Name = "الثوابت",
                    NameEn = "Constants",
                    Icon = "fa fa-anchor",
                    Link = "Constant/Index",
                    InMenu = true,
                    ParentId = 3, //Management
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Page
                }

            };

            pages.AddRange(ToolPagesSeed.AddToollPages(pages.Last().Id)); // add Tool Pages  

            builder.Entity<Page>().HasData(pages);

            // Seed Admin User and all permissions  
            builder.Seed(pages);
        }
    }
}