using Acadimia.Data.Enums;

namespace Acadimia.Data.Models
{
    public class Lesson : BaseModel
    {
        public int Id { get; set; }
        public int? GroupId { get; set; }
        public Group? Group { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
        public string Title { get; set; }        
        public int OrderIndex { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public LessonStatus Status { get; set; } = LessonStatus.Scheduled;
        public MeetingPlatform? MeetingPlatform { get; set; }
        public string? MeetingUrl { get; set; }
        public string? MeetingInstructions { get; set; }
        public string? CancellationReason { get; set; }

        public string? Room { get; set; }
    }
}