using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    public class Group : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public int MaxStudents { get; set; }
        public DateTime? CourseStartDate { get; set; }
        public DateTime? CourseEndDate { get; set; }
        public int DefaultLessonDurationMinutes { get; set; }
    }
}