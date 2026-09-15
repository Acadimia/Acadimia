namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public string? Qualifications { get; set; }
        public int ExperienceYears { get; set; }
        public string? ServiceArea { get; set; }
        public string? Languages { get; set; }
        public bool SupportsOnline { get; set; }
        public bool SupportsInPerson { get; set; }
        public decimal? HourlyPriceOnline { get; set; }
        public decimal? HourlyPriceInPerson { get; set; }
        public string? ProfileImage { get; set; }
        public List<string> Subjects { get; set; } = new();
        public List<string> Grades { get; set; } = new();
        public double? AverageRating { get; set; }
        public int RatingCount { get; set; }
    }
}