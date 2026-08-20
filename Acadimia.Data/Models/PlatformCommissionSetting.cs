using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // Versioned platform commission percentage applied to instructor earnings.
    // History is kept (EffectiveFrom/EffectiveTo) for audit; CreatedBy (from
    // BaseModel) records which admin set the rate.
    public class PlatformCommissionSetting : BaseModel
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal CommissionPercentage { get; set; }

        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        // Convenience flag for the currently-applied rate.
        public bool IsActive { get; set; }
    }
}
