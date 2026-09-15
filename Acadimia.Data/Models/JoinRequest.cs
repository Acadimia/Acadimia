using Acadimia.Data.Enums;

namespace Acadimia.Data.Models
{
    // A student's request to join a live Group or enroll in a pre-recorded
    // Course, awaiting instructor decision. Produces one Enrollment row on approval.
    public class JoinRequest : BaseModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public JoinRequestTargetType TargetType { get; set; }

        // Exactly one of GroupId / CourseId is set, matching TargetType.
        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public JoinRequestStatus Status { get; set; }

        public string? DecisionBy { get; set; }
        public User? DecisionByUser { get; set; }
        public DateTime? DecisionOn { get; set; }
        public string? RejectionReason { get; set; }
    }
}
