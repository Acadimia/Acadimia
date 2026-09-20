using Acadimia.Data.Enums;
namespace Acadimia.Infrastructure.Dtos.Courses
{
    public class CourseListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public CourseDeliveryType DeliveryType { get; set; }
        public CourseStatus Status { get; set; }
        public int MaxStudents { get; set; }
        public int TeacherId { get; set; }
        public string? TeacherName { get; set; }
        public string? SubjectName { get; set; }
        public string? CategoryName { get; set; }
    }
}