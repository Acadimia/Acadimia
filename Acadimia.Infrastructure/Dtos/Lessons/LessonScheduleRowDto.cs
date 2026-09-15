using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.Lessons
{
    public class LessonScheduleRowDto
    {
        public int LessonId { get; set; }
        public CourseDeliveryType? CourseType { get; set; }
        public string Topic { get; set; }
        public DateTime Date { get; set; }
        public DayOfWeek Day { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }

        public string PlatformOrRoom { get; set; }
    }
}