// WithdrawalDecisionDto.cs
using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Wallet
{
    // Admin/Finance يوافق أو يرفض قبل تحويل الفلوس فعلياً
    public class WithdrawalDecisionDto
    {
        [Required]
        public int RequestId { get; set; }

        [Required]
        public bool Approve { get; set; }

        public string? RejectionReason { get; set; }
    }
}