using System.ComponentModel.DataAnnotations;
using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.Lessons
{
    public class LessonInputDto
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }
        public int? CourseId { get; set; }

        [Required, StringLength(250, MinimumLength = 3)]
        public string Title { get; set; } 

        [Required]
        public DateTime ScheduledDate { get; set; }

        [Required]
        public TimeSpan StartTime { get; set; }

        [Required]
        public int DurationMinutes { get; set; }

        public int OrderIndex { get; set; }

        public MeetingPlatform? MeetingPlatform { get; set; }
        public string? MeetingUrl { get; set; }
        public string? MeetingInstructions { get; set; }
        public string? Room { get; set; } 
    }

    public class MeetingConfigInputDto
    {
        [Required]
        public int LessonId { get; set; }

        [Required]
        public MeetingPlatform MeetingPlatform { get; set; }

        [Required, Url]
        public string MeetingUrl { get; set; }

        public string? MeetingInstructions { get; set; }
    }

    public class LessonCancelDto
    {
        [Required]
        public int LessonId { get; set; }

        public string? Reason { get; set; }
    }
}