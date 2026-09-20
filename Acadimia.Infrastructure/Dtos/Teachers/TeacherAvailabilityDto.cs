using Acadimia.Data.Enums;
namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherAvailabilityDto
    {
        public int Id { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public CourseDeliveryType TeachingMode { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
    }
}