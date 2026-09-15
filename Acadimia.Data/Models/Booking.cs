using Acadimia.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    public class Booking : BaseModel
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }

        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }

        public CourseDeliveryType TeachingMode { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public BookingStatus Status { get; set; }

        public string? StudentNote { get; set; }
        public string? RejectionReason { get; set; }

        public DateTime? PaidOn { get; set; }
    }
}