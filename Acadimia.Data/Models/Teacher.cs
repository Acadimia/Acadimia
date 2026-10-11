using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Acadimia.Data.Models
{
    public class Teacher : BaseModel
    {
        public int Id { get; set; }

        public int? GradeId { get; set; }
        public Grade? Grade { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string? Bio { get; set; }
        public string? Qualifications { get; set; }
        public int ExperienceYears { get; set; }
        public string? ServiceArea { get; set; }
        public string? Languages { get; set; } 
        public bool SupportsOnline { get; set; }
        public bool SupportsInPerson { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? HourlyPriceOnline { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? HourlyPriceInPerson { get; set; }

        public string? ProfileImage { get; set; }
        public bool IsPublicForDiscovery { get; set; }
    }
}