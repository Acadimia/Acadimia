using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Wallet
{
    public class VerifyTopUpRequestDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public bool Approve { get; set; }

        public string? RejectionReason { get; set; }
    }
}