using Acadimia.Core.Enums;

namespace Acadimia.Data.Models
{
    // Per-session attendance record for a student in a group.
    public class Attendance : BaseModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }

        public DateTime SessionDate { get; set; }
        public AttendanceStatus Status { get; set; }

        public string RecordedBy { get; set; }
        public User RecordedByUser { get; set; }

        public string? Notes { get; set; }
    }
}
