using Acadimia.Data.Enums;
using Acadimia.Data.Models;

namespace Acadimia.Data.SeedHeper.PageSeed
{

    public static class RolePagesSeed
    {
        private const int Admin = UserTypeIds.Admin;
        private const int Student = UserTypeIds.Student;
        private const int Teacher = UserTypeIds.Teacher;
        private const int Parent = UserTypeIds.Parent;

        private sealed record Def(int Id, string Name, string NameEn, string Icon, string Link, int[] Roles);

        private static readonly Def[] Defs =
        {
            // ---------------- الطالب ----------------
            new(100, "لوحة الطالب",        "Student Dashboard",   "bi bi-speedometer2",   "Student/Dashboard",      new[] { Student }),
            new(101, "البحث عن معلم",      "Find a Teacher",      "bi bi-search",         "Teacher/Search",         new[] { Student }),
            new(102, "الدورات المتاحة",    "Available Courses",   "bi bi-collection",     "Course/Published",       new[] { Student }),
            new(103, "طلبات الانضمام",     "My Join Requests",    "bi bi-person-plus",    "JoinRequest/MyRequests", new[] { Student }),
            new(104, "طلبات الحجز",        "My Requests",         "bi bi-hourglass-split","Student/MyRequests",     new[] { Student }),
            new(105, "حجوزاتي",            "My Bookings",         "bi bi-calendar-check", "Student/MyBookings",     new[] { Student }),
            new(106, "جدولي",              "My Schedule",         "bi bi-calendar3",      "Student/Schedule",       new[] { Student }),
            new(107, "الحضور",             "Attendance",          "bi bi-clipboard-check","Student/Attendance",     new[] { Student }),
            new(108, "الامتحانات والنتائج","Exams & Results",     "bi bi-journal-text",   "Student/ExamResults",    new[] { Student }),
            new(109, "التقدم الأكاديمي",   "Academic Progress",   "bi bi-graph-up",       "Student/Progress",       new[] { Student }),
            new(110, "الإشعارات",          "Notifications",       "bi bi-bell",           "Student/Notifications",  new[] { Student }),

            // ------- مشترك (طالب + معلم) -------
            new(111, "المحفظة",            "Wallet",              "bi bi-wallet2",        "Wallet/MyWallet",        new[] { Student, Teacher }),

            // ---------------- المعلم ----------------
            new(120, "دوراتي",             "My Courses",          "bi bi-book",           "Course/GetAll",          new[] { Teacher }),
            new(121, "طلبات الانضمام الواردة","Join Requests Inbox","bi bi-inbox",         "JoinRequest/Pending",    new[] { Teacher, Admin }),
            new(122, "طلبات الحجز الواردة","Booking Requests",    "bi bi-calendar-plus",  "Booking/TeacherBookings",new[] { Teacher }),
            new(123, "طلبات إعادة الجدولة","Reschedule Requests", "bi bi-arrow-repeat",   "Booking/TeacherRescheduleRequests", new[] { Teacher }),
            new(124, "جدول الدروس",        "Lessons Schedule",    "bi bi-calendar-week",  "Lesson/GetSchedule",     new[] { Teacher }),
            new(125, "ملفي التعريفي",      "Teacher Profile",     "bi bi-person-badge",   "Teacher/UpdateProfile",  new[] { Teacher }),
            new(126, "أوقات التوفر",       "Availability",        "bi bi-clock",          "Teacher/SetAvailability",new[] { Teacher }),

            // ---------------- ولي الأمر ----------------
            new(140, "أبنائي",             "My Children",         "bi bi-people",         "Parent/MyChildren",      new[] { Parent }),

            // ---------------- إضافات للأدمن ----------------
            new(150, "طلبات شحن الرصيد",   "Top-Up Requests",     "bi bi-cash-coin",      "Wallet/GetPendingTopUpRequests",     new[] { Admin }),
            new(151, "طلبات السحب",        "Withdrawal Requests", "bi bi-bank",           "Wallet/GetPendingWithdrawalRequests",new[] { Admin }),
            new(152, "نسبة العمولة",       "Platform Commission", "bi bi-percent",        "PlatformCommission/Current",         new[] { Admin }),
            new(153, "ربط أولياء الأمور",  "Parent Links",        "bi bi-link-45deg",     "ParentLink/List",                    new[] { Admin }),
        };

        public static List<Page> GetPages() => Defs.Select(d => new Page
        {
            Id = d.Id,
            Name = d.Name,
            NameEn = d.NameEn,
            Icon = d.Icon,
            Link = d.Link,
            InMenu = true,
            IsAjax = false,
            IsActive = true,
            ParentId = (int)GeneralEnums.ParentPageId,
            ModuleId = null, // بدون Module حتى لا تختفي إذا تعطلت وحدة
            CategoryId = (int)GeneralEnums.Page
        }).ToList();

        // ids الصفحات الافتراضية لنوع مستخدم معيّن
        public static List<int> DefaultPageIdsFor(int userTypeId) =>
            Defs.Where(d => d.Roles.Contains(userTypeId)).Select(d => d.Id).ToList();

        // صفحات خاصة بطالب/معلم/ولي أمر فقط، ما بدنا تظهر بقائمة الأدمن
        public static HashSet<int> NonAdminPageIds { get; } =
            Defs.Where(d => !d.Roles.Contains(Admin)).Select(d => d.Id).ToHashSet();
    }
}
