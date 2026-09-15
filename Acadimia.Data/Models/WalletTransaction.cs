using Acadimia.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    
    public class WalletTransaction : BaseModel
    {
        public int Id { get; set; }

        public int WalletId { get; set; }
        public Wallet Wallet { get; set; }

        public WalletTransactionDirection Direction { get; set; }
        public WalletTransactionType Type { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public WalletTransactionStatus Status { get; set; }

        // Human-readable line for the history list, e.g.
        // "Withdrawal to Bank Al-Ahli - Acc. ****1234" or "Enrollment fee - Grade 10 Math".
        public string? Description { get; set; }

        // Points back at the source record: "TopUpRequest" / "WithdrawalRequest" / "Enrollment".
        public string? RelatedEntityType { get; set; }
        public int? RelatedEntityId { get; set; }

        // Admin/Finance Officer who accepted/rejected this entry (top-up & withdrawal only).
        public string? DecisionBy { get; set; }
        public User? DecisionByUser { get; set; }
        public DateTime? DecisionOn { get; set; }
    }
}
