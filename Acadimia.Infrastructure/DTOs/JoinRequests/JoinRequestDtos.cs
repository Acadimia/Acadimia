using System.ComponentModel.DataAnnotations;
using Acadimia.Data.Enums;

namespace Acadimia.Infrastructure.Dtos.JoinRequests
{
    public class JoinRequestInputDto
    {
        [Required] public JoinRequestTargetType TargetType { get; set; }
        public int? GroupId { get; set; }
        public int? CourseId { get; set; }
    }

    public class JoinRequestDecisionDto
    {
        [Required] public int RequestId { get; set; }
        [Required] public bool Approve { get; set; }
        public string? RejectionReason { get; set; }
    }

    public class JoinRequestDto
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string? StudentName { get; set; }
        public JoinRequestTargetType TargetType { get; set; }
        public int? GroupId { get; set; }
        public int? CourseId { get; set; }
        public string? Title { get; set; }
        public decimal Fee { get; set; }
        public JoinRequestStatus Status { get; set; }
        public string? RejectionReason { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}