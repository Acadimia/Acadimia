using System.ComponentModel.DataAnnotations;
using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherAvailabilitySlotDto
    {
        [Required] public DayOfWeek DayOfWeek { get; set; }
        [Required] public TimeSpan StartTime { get; set; }
        [Required] public TimeSpan EndTime { get; set; }
        [Required] public CourseDeliveryType TeachingMode { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }

    public class TeacherAvailabilityInputDto
    {
        [Required, MinLength(1)]
        public List<TeacherAvailabilitySlotDto> Slots { get; set; } = new();
    }
}