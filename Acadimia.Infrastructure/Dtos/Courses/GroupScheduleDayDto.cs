using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Courses
{
    public class GroupScheduleDayDto
    {
        [Required]
        public DayOfWeek DayOfWeek { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }
    }

    public class GroupScheduleInputDto
    {
        [Required]
        public int GroupId { get; set; }

        [Required]
        public int MaxStudents { get; set; }

        public DateTime? CourseStartDate { get; set; }
        public DateTime? CourseEndDate { get; set; }

        [Required]
        public int DefaultLessonDurationMinutes { get; set; }

        [Required, MinLength(1)]
        public List<GroupScheduleDayDto> ScheduleDays { get; set; } = new();
    }
}