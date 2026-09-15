using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Acadimia.Data.Models
{
    public class Teacher : BaseModel
    {
        public int Id { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        // Login account this profile belongs to - required to resolve "which
        // logged-in user is this Teacher" for ownership checks (FR-T08/T09/T10).
        public string? UserId { get; set; }
        public User? User { get; set; }

        // ---- Public discovery profile (FR-T03/FR-T08) ----
        public string? Bio { get; set; }
        public string? Qualifications { get; set; }
        public int ExperienceYears { get; set; }
        public string? ServiceArea { get; set; }
        public string? Languages { get; set; } // comma-separated; a lookup table can replace this later
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