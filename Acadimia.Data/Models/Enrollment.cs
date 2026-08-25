using Acadimia.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // Active roster linking a Student to a Group or Course after a JoinRequest
    // is approved. This is the many-to-many relationship the previous schema
    // was missing entirely - nothing connected Students to Groups before this.
    public class Enrollment : BaseModel
    {
        public int Id { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public int JoinRequestId { get; set; }
        public JoinRequest JoinRequest { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal FeeAmount { get; set; }

        public DateTime EnrollmentDate { get; set; }
        public EnrollmentStatus Status { get; set; }
    }
}
