using Acadimia.Core.Enums;

namespace Acadimia.Data.Models
{
    public class TeacherAvailability : BaseModel
    {
        public int Id { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public CourseDeliveryType TeachingMode { get; set; }

        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }

        public bool IsActive { get; set; } = true;
    }
}