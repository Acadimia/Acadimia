using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Commission
{
    public class SetCommissionInputDto
    {
        [Required]
        [Range(0, 100, ErrorMessage = "نسبة العمولة يجب أن تكون بين 0 و 100")]
        public decimal CommissionPercentage { get; set; }
    }

    public class CommissionSettingDto
    {
        public int Id { get; set; }
        public decimal CommissionPercentage { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public bool IsActive { get; set; }
        public string? SetBy { get; set; }      // اسم الأدمن الذي حدّد النسبة
    }

    public class CurrentCommissionDto
    {
        public decimal CommissionPercentage { get; set; }
        // true = لا توجد نسبة محفوظة، والمعروضة هي القيمة الاحتياطية
        public bool IsDefault { get; set; }
        public DateTime? EffectiveFrom { get; set; }
    }
}