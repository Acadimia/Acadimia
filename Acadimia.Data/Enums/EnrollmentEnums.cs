namespace Acadimia.Data.Enums
{
    public enum CourseStatus
    {
        Draft = 1,
        Published = 2,
        Archived = 3
    }

    // Which of Group / Course a JoinRequest (or Enrollment) points to.
    public enum JoinRequestTargetType
    {
        Group = 1,
        Course = 2
    }

    public enum JoinRequestStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3
    }

    public enum EnrollmentStatus
    {
        Active = 1,
        Completed = 2,
        Cancelled = 3
    }

    public enum LessonMaterialFileType
    {
        Pdf = 1,
        Video = 2,
        Image = 3,
        Other = 4
    }
}