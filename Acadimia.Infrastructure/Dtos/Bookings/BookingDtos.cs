using System.ComponentModel.DataAnnotations;
using Acadimia.Core.Enums;

namespace Acadimia.Infrastructure.Dtos.Bookings
{
    public class BookingInputDto
    {
        [Required] public int TeacherId { get; set; }
        public int? SubjectId { get; set; }
        public int? GradeId { get; set; }

        [Required] public CourseDeliveryType TeachingMode { get; set; }
        [Required] public DateTime Date { get; set; }
        [Required] public TimeSpan StartTime { get; set; }
        [Required, Range(15, 480)] public int DurationMinutes { get; set; }

        public string? StudentNote { get; set; }
    }

    public class BookingDecisionDto
    {
        [Required] public int BookingId { get; set; }
        [Required] public bool Accept { get; set; }
        public string? RejectionReason { get; set; }
    }

    public class RateTeacherDto
    {
        [Required] public int BookingId { get; set; }
        [Required, Range(1, 5)] public int RatingValue { get; set; }
        public string? Review { get; set; }
    }

    public class BookingDto
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public int? SubjectId { get; set; }
        public string? SubjectName { get; set; }
        public CourseDeliveryType TeachingMode { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationMinutes { get; set; }
        public decimal Price { get; set; }
        public BookingStatus Status { get; set; }
        public string? StudentNote { get; set; }
        public string? RejectionReason { get; set; }
    }
}