using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // An exam scheduled for a Group or Course.
    public class Exam : BaseModel
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public string Title { get; set; }
        public DateTime ExamDate { get; set; }

        [Column(TypeName = "decimal(6,2)")]
        public decimal TotalMarks { get; set; }
    }
}
