using Acadimia.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    public class Course : BaseModel
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int? SubjectId { get; set; }
        public Subject? Subject { get; set; }

        public int CategoryId { get; set; }
        public CourseCategory Category { get; set; }

        public string Title { get; set; }
        public string? Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public CourseStatus Status { get; set; }

        public CourseDeliveryType DeliveryType { get; set; }
        public int MaxStudents { get; set; }
    }
}