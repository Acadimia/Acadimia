namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherReviewDto
    {
        public int Id { get; set; }
        public int RatingValue { get; set; }
        public string? Review { get; set; }
        public string? StudentName { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}