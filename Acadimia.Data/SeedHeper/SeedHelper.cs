using Acadimia.Data.Enums;
using Acadimia.Data.Models;
using Acadimia.Data.SeedHeper;
using Acadimia.Data.SeedHeper.PageSeed;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.SeedHeper
{
    public static class SeedHelper
    {
        public static void Seed(this ModelBuilder builder)
        {
            SeedPageCategories(builder);
            SeedModules(builder);
            SeedConstants(builder);
            PagesSeed.Seed(builder);
            NationalitySeed.Seed(builder);
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
                new Constant { Id = 1, Name = "العملة" }, // Currency
                new Constant { Id = 2, Name = "دولار", ParentId = 1 },
                new Constant { Id = 3, Name = "دينار", ParentId = 1 },
                new Constant { Id = 4, Name = "شيكل", ParentId = 1 },

                new Constant { Id = 5, Name = "الجنس" }, // Gender
                new Constant { Id = 6, Name = "ذكر", ParentId = 5 },
                new Constant { Id = 7, Name = "أنثى", ParentId = 5 },

                new Constant { Id = 8, Name = "نوع المكان المقصود" }, // DestinationType
                new Constant { Id = 9, Name = "دولة", ParentId = 8 }, // Country
                new Constant { Id = 10, Name = "مدينة", ParentId = 8 }, // City
                new Constant { Id = 11, Name = "محافظة", ParentId = 8 }, // Province

                new Constant { Id = 12, Name = "نوع المرفق" }, // AttachmentType
                new Constant { Id = 13, Name = "جواز سفر", ParentId = 12 }, // PassportAttachment
                new Constant { Id = 14, Name = "هوية", ParentId = 12 }, // IdentityAttachment
                new Constant { Id = 15, Name = "شهادة ثانوية عامة", ParentId = 12 },
                new Constant { Id = 16, Name = "شهادة دبلوم", ParentId = 12 },
                new Constant { Id = 17, Name = "شهادة بكالوريس", ParentId = 12 },
                new Constant { Id = 18, Name = "شهادة ماجستير", ParentId = 12 }

                //new Constant { Id = 20, Name = "نوع المستفيد" }, // BeneficiaryType
                //new Constant { Id = 21, Name = "طال", ParentId = 20 },
                //new Constant { Id = 22, Name = "مورد", ParentId = 20 },
                //new Constant { Id = 23, Name = "مزود خدمة", ParentId = 20 },

                
                //new Constant { Id = 27, Name = "نوع العنوان" }, // AddressType
                //new Constant { Id = 28, Name = "عنوان السكن", ParentId = 27 },
                //new Constant { Id = 29, Name = "عنوان العمل", ParentId = 27 },
                //new Constant { Id = 30, Name = "العنوان التعليمي", ParentId = 27 },
                //new Constant { Id = 31, Name = "عنوان مؤقت", ParentId = 27 },
                //new Constant { Id = 32, Name = "عنوان الشحن", ParentId = 27 },

                //new Constant { Id = 33, Name = "نوع جهة الاتصال" }, // ContactType
                //new Constant { Id = 34, Name = "رقم موبايل", ParentId = 33 },
                //new Constant { Id = 35, Name = "رقم هاتف", ParentId = 33 },
                //new Constant { Id = 36, Name = "رقم واتساب", ParentId = 33 },
                //new Constant { Id = 37, Name = "رقم تيلجرام", ParentId = 33 },

                //new Constant { Id = 38, Name = "نوع الهوية" }, // IdentityType
                //new Constant { Id = 39, Name = "بطاقة تعريفية", ParentId = 38 },
                //new Constant { Id = 40, Name = "هوية مدنية", ParentId = 38 },

                //new Constant { Id = 41, Name = "الديانة" }, // Religion
                //new Constant { Id = 42, Name = "الإسلام", ParentId = 41 },
                //new Constant { Id = 43, Name = "المسيحية", ParentId = 41 },

                //new Constant { Id = 44, Name = "صلة القرابة" }, // Kinship
                //new Constant { Id = 45, Name = "اب", ParentId = 44 },
                //new Constant { Id = 46, Name = "ام", ParentId = 44 },
                //new Constant { Id = 47, Name = "ابن", ParentId = 44 },
                //new Constant { Id = 48, Name = "بنت", ParentId = 44 },
                //new Constant { Id = 49, Name = "زوج", ParentId = 44 },
                //new Constant { Id = 50, Name = "زوجة", ParentId = 44 },

                //new Constant { Id = 51, Name = "تصنيف المستفيد" }, // BeneficiaryCategory
                //new Constant { Id = 52, Name = "موثوق", ParentId = 51 },
                //new Constant { Id = 53, Name = "غير موثوق", ParentId = 51 },
                //new Constant { Id = 54, Name = "غير مصنف", ParentId = 51 },

                //new Constant { Id = 55, Name = "مجموعات الخدمات" }, // Service Group
                //new Constant { Id = 56, Name = "خدمات جوازات السفر", ParentId = 55 },
                //new Constant { Id = 57, Name = "خدمات الهويات", ParentId = 55 },
                //new Constant { Id = 58, Name = "خدمات العقود", ParentId = 55 },
                //new Constant { Id = 59, Name = "خدمات التوثيقات", ParentId = 55 },

                //new Constant { Id = 60, Name = "الأولوية", }, // Priority
                //new Constant { Id = 61, Name = "إجباري", ParentId = 60 },
                //new Constant { Id = 62, Name = "إختياري", ParentId = 60 },

                //new Constant { Id = 63, Name = "نوع الحسابات" }, // Accounts Types
                //new Constant { Id = 64, Name = "مصروفات", ParentId = 63 }, // Expense Accounts
                //new Constant { Id = 65, Name = "مصروفات", ParentId = 63 }, // Revenue Accounts
                //new Constant { Id = 66, Name = "ذمم مدينة", ParentId = 63 },
                //new Constant { Id = 67, Name = "ذمم دائنة", ParentId = 63 },
                //new Constant { Id = 68, Name = "صناديق", ParentId = 63 }, // Box Accounts
                //new Constant { Id = 69, Name = "بنوك", ParentId = 63 }, // Bank Accounts
                //new Constant { Id = 70, Name = "مشتريات", ParentId = 63 },
                //new Constant { Id = 71, Name = "مبيعات", ParentId = 63 },

                //new Constant { Id = 72, Name = " انواع الجهات" }, // Agency Types
                //new Constant { Id = 73, Name = "جهات حكومية", ParentId = 72 },
                //new Constant { Id = 74, Name = "جهات خدماتية", ParentId = 72 },
                //new Constant { Id = 75, Name = "مندوب شركات", ParentId = 72 },
                //new Constant { Id = 76, Name = "شركات خاصة", ParentId = 72 },

                //new Constant { Id = 77, Name = " حالات الطلب" }, // Request Cases
                //new Constant { Id = 78, Name = "جديد", ParentId = 77 },
                //new Constant { Id = 79, Name = "قيدالتنفيذ", ParentId = 77 },
                //new Constant { Id = 80, Name = "جاري الإرسال", ParentId = 77 },
                //new Constant { Id = 81, Name = "جاري الإستلام", ParentId = 77 },
                //new Constant { Id = 82, Name = "الإستلام من المكتب", ParentId = 77 },
                //new Constant { Id = 83, Name = "توصيل مع الديلفري", ParentId = 77 },
                //new Constant { Id = 84, Name = "تم الإستلام", ParentId = 77 },
                //new Constant { Id = 85, Name = "معلق", ParentId = 77 },
                //new Constant { Id = 86, Name = "ملغي", ParentId = 77 },

                //new Constant { Id = 87, Name = "حالة الفاتورة" }, // Invoice Status
                //new Constant { Id = 88, Name = "جديد", ParentId = 87 },
                //new Constant { Id = 89, Name = "مرحل", ParentId = 87 },
                //new Constant { Id = 90, Name = "معدل", ParentId = 87 },
                //new Constant { Id = 91, Name = "لغي", ParentId = 87 }

            );
        }

     
    }
}
