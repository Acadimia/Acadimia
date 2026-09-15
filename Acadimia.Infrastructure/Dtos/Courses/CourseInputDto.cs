using System.ComponentModel.DataAnnotations;
using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.Courses
{
    public class CourseInputDto
    {
        public int Id { get; set; }

        [Required]
        public int TeacherId { get; set; }
        public int? SubjectId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required, StringLength(250, MinimumLength = 3)]
        public string Title { get; set; }
        public string? Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public CourseDeliveryType DeliveryType { get; set; }

        [Required]
        public int MaxStudents { get; set; }

        // Group created alongside the course (FR-I01/I05 "group name/number")
        [Required, StringLength(150, MinimumLength = 1)]
        public string GroupName { get; set; }

        [Required]
        public int GradeId { get; set; }

        // true = save as Draft, false = publish immediately
        public bool SaveAsDraft { get; set; }
    }
}