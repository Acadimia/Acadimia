using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Teachers
{
  
    public class TeacherProfileInputDto
    {
        public string? Bio { get; set; }
        public string? Qualifications { get; set; }

        [Range(0, 60)]
        public int ExperienceYears { get; set; }

        public string? ServiceArea { get; set; }
        public string? Languages { get; set; }

        public bool SupportsOnline { get; set; }
        public bool SupportsInPerson { get; set; }

        public decimal? HourlyPriceOnline { get; set; }
        public decimal? HourlyPriceInPerson { get; set; }

        public bool IsPublicForDiscovery { get; set; }

        public List<int> SubjectIds { get; set; } = new();
        public List<int> GradeIds { get; set; } = new();
    }
}