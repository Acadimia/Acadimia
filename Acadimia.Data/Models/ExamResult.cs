using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // A student's score on an Exam. Distinct from the existing Grade entity,
    // which represents class level, not an academic score.
    public class ExamResult : BaseModel
    {
        public int Id { get; set; }

        public int ExamId { get; set; }
        public Exam Exam { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal ScoreObtained { get; set; }

        public string? Feedback { get; set; }

        public string GradedBy { get; set; }
        public User GradedByUser { get; set; }
        public DateTime? GradedOn { get; set; }
    }
}
