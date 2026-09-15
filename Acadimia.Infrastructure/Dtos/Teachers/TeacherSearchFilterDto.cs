namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherSearchFilterDto
    {
        public string? Keyword { get; set; }
        public int? SubjectId { get; set; }
        public int? GradeId { get; set; }
        public string? ServiceArea { get; set; }
        public bool? Online { get; set; }
        public bool? InPerson { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public double? MinRating { get; set; }
        public int? MinExperienceYears { get; set; }
        public string? Language { get; set; }
        public DayOfWeek? AvailableDay { get; set; }
        public int Skip { get; set; }
        public int PageSize { get; set; } = 10;
    }
}