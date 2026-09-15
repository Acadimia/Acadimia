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
        public string? Feedback { get; set; }
    }
}