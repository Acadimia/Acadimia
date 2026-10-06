using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Parent
{
    public class ChildOverviewDto
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string GradeName { get; set; }
        public decimal AttendanceRatePercent { get; set; }
        public decimal? AverageExamScorePercent { get; set; }
        public int UnreadNotificationsCount { get; set; }
    }

    public class ChildAttendanceRowDto
    {
        public DateTime SessionDate { get; set; }
        public string GroupName { get; set; }
        public Acadimia.Data.Enums.AttendanceStatus Status { get; set; }
        public string? Notes { get; set; }
    }

    public class ChildExamResultRowDto
    {
        public string ExamTitle { get; set; }
        public DateTime ExamDate { get; set; }
        public decimal ScoreObtained { get; set; }
        public decimal TotalMarks { get; set; }
        public decimal? Percentage { get; set; }   // null عندما TotalMarks = 0
        public string? Feedback { get; set; }
    }

    // ---- FR-P09 / FR-P10 (إدارة الربط من الأدمن) ----
    public class ParentLinkInputDto
    {
        [Required] public string ParentUserId { get; set; }
        [Required] public int StudentId { get; set; }
        public int? RelationTypeId { get; set; }
        public bool IsPrimaryContact { get; set; }
    }

    public class ParentLinkUpdateDto
    {
        [Required] public int LinkId { get; set; }
        public int? RelationTypeId { get; set; }
        public bool IsPrimaryContact { get; set; }
    }

    public class ParentLinkDto
    {
        public int Id { get; set; }
        public string ParentUserId { get; set; }
        public string? ParentName { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public int? RelationTypeId { get; set; }
        public string? RelationTypeName { get; set; }
        public bool IsPrimaryContact { get; set; }
    }
}