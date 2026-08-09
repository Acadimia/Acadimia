using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.SeedHeper.PageSeed
{
    public static class ToolPagesSeed
    {
        public static List<Page> AddToollPages(int lastPageId)
        {
            var toolPages = new List<Page>()
            {
                new Page()
                {
                    Name = "عرض بيانات جدول المستخدمين",
                    NameEn = "Display User DataTable",
                    Icon = null,
                    Link ="User/GetAll",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "اظهار واجهة اضافة  تعديل مستخدم",
                    NameEn = "Display Create Edit User Page",
                    Icon = null,
                    Link ="User/CreateEditModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "اضافة تعديل مستخدم",
                    NameEn = "Create Edit User",
                    Icon = null,
                    Link ="User/CreateEdit",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "حذف مستخدم",
                    NameEn = "Delete User",
                    Icon = null,
                    Link ="User/Delete",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض واجهة ملفي الشخصي",
                    NameEn = "Display My Profile Page",
                    Icon = null,
                    Link ="User/MyProfileModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "تعديل ملفي الشخصي",
                    NameEn = "Update My Profile",
                    Icon = null,
                    Link ="User/MyProfile",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض واجهة تغير كلمة المرور",
                    NameEn = "Display Change Password Page",
                    Icon = null,
                    Link ="User/ChangePasswordModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "تغير كلمة المرور",
                    NameEn = "ChangePassword",
                    Icon = null,
                    Link ="User/ChangePassword",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPageId, // User Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض بيانات جدول انواع المستخدين",
                    NameEn = "Display User Type DateTable",
                    Icon = null,
                    Link ="UserType/GetAll",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserTypePageId, // UserType Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض واجهة اضافة  تعديل نوع المستخدم",
                    NameEn = "Display Create Edit User Type page",
                    Icon = null,
                    Link ="UserType/CreateEditModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserTypePageId, // UserType Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "اضافة تعديل نوع مستخدم",
                    NameEn = "Create Edit User Type ",
                    Icon = null,
                    Link ="UserType/CreateEdit",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserTypePageId, // UserType Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "حذف نوع مستخدم",
                    NameEn =  "Delete User Type ",
                    Icon = null,
                    Link ="UserType/Delete",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserTypePageId, // UserType Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض صلاحيات نوع المستخدم",
                    NameEn =  "display User Type Permissions",
                    Icon = null,
                    Link ="UserPermission/GetUserTypePermissions",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPermissionsId, // User Permissions Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "حفظ صلاحيات نوع المستخدم",
                    NameEn =  "Save User Type Permissions",
                    Icon = null,
                    Link ="UserPermission/SavePermissions",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.UserPermissionsId, // User Permissions Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض بيانات جدول المحافظات والمدن",
                    NameEn =  "Display Governorates and Cities DateTable",
                    Icon = null,
                    Link ="Destination/GetAll",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.DestinationId, // Destinations Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض واجهة إضافة تعديل وجهة",
                    NameEn =  "Display create Edit Destination page",
                    Icon = null,
                    Link ="Destination/CreateEditModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.DestinationId, // Destinations Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "إضافة تعديل وجهة",
                    NameEn =  "create Edit Destination",
                    Icon = null,
                    Link ="Destination/CreateEdit",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.DestinationId, // Destinations Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "حذف وجهة",
                    NameEn =  "Delete Destination",
                    Icon = null,
                    Link ="Destination/Delete",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.DestinationId, // Destinations Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "تبديل حالات وحدات النظام",
                    NameEn =  "Switching states of system Modules",
                    Icon = null,
                    Link ="Management/SwitchStatus",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.SystemModulesId, // System Modules Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض بيانات جدول الصفحات",
                    NameEn =  "Display Pages DataTable",
                    Icon = null,
                    Link ="Page/GetAll",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.PageId, // System Modules Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض واجهة إضافة  تعديل صفحة",
                    NameEn =  "Display Create Edit Page interface",
                    Icon = null,
                    Link ="Page/CreateEditModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.PageId, // System Modules Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "إضافة تعديل صفحة",
                    NameEn =  "Create Edit Page",
                    Icon = null,
                    Link ="Page/CreateEdit",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.PageId, // System Modules Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "حذف صفحة",
                    NameEn =  "Delete Page",
                    Icon = null,
                    Link ="Page/Delete",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.PageId, // System Modules Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض بيانات جدول الثوابت",
                    NameEn =  "Display Constant DataTable",
                    Icon = null,
                    Link ="Constant/GetAll",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.ConstantId, // Constant Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "عرض واجهة إضافة تعديل ثوابت",
                    NameEn =  "Display Create Edit Constant Page",
                    Icon = null,
                    Link ="Constant/CreateEditModal",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.ConstantId, // Constant Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "إضافة تعديل ثوابت",
                    NameEn =  "Create Edit Constant",
                    Icon = null,
                    Link ="Constant/CreateEdit",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.ConstantId, // Constant Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },
                new Page()
                {
                    Name = "حذف ثابت",
                    NameEn =  "Delete Constant",
                    Icon = null,
                    Link ="Constant/Delete",
                    InMenu = false,
                    IsAjax = true,
                    ParentId = (int)GeneralEnums.ConstantId, // Constant Page
                    IsActive = true,
                    ModuleId = (int)GeneralEnums.Management,
                    CategoryId = (int)GeneralEnums.Tool
                },

                  };


            lastPageId++;
            foreach (var page in toolPages)
            {
                page.Id = lastPageId++;
            }

            return toolPages;
        }
    }
}