using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.Enums
{

    public static class UserTypeIds
    {
        public const int Admin = 1;
        public const int Student = 3;
        public const int Teacher = 4;
        public const int Parent = 5;

        // الأنواع المسموح للزائر يسجّل نفسه فيها. الأدمن (1) ممنوع نهائيًا.
        public static readonly int[] SelfRegistration = { Student, Teacher, Parent };
    }
    public enum GeneralEnums
    {
        Gender = 5,
        Kinship = 20,

        Male = 6,   // Gender
        Female = 7,

        ParentPageId = 1,   // Parent Page Id

        Header = 1, // Page Category
        Page = 2,
        Tool = 3,

        Management = 1, //Modules

        // Parent Page Ids
        UserPageId = 5,
        UserTypePageId = 6,
        UserPermissionsId = 7,
        DestinationId = 8,
        SystemModulesId = 9,
        PageId = 10,
        ConstantId = 11,

    }
    //    public enum AttendanceStatus
    //{
    //    Present = 1,
    //    Absent = 2,
    //    Late = 3,
    //    Excused = 4
    //}

    //public enum NotificationType
    //{
    //    JoinRequest = 1,
    //    Wallet = 2,
    //    Schedule = 3,
    //    System = 4,
    //    Booking = 5
    //}
    public enum BookingStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Cancelled = 4,
        Confirmed = 5,
        Completed = 6
    }
}

