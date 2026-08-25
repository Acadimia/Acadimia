using Acadimia.Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // The single unified feed behind the wallet's transaction history screen.
    // A row is created the moment a top-up or withdrawal request is submitted
    // (Status = Pending) and is kept in sync as Admin/Finance decides on it
    // (Accepted / Rejected / Completed), so the wallet UI can show a full
    // "received" (Direction = In) and "sent" (Direction = Out) history with a
    // clear status on every row - including instructor withdrawals to their bank.
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
