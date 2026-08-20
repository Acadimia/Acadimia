using Acadimia.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // Student-submitted, manually-verified bank-transfer top-up request.
    // Creates one WalletTransaction row on submit (Direction=In, Type=TopUp,
    // Status=Pending); approval credits Wallet.Balance and flips it to Accepted,
    // rejection flips it to Rejected.
    public class WalletTopUpRequest : BaseModel
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string BankReferenceNo { get; set; }

        // Uploaded receipt image/PDF (access-restricted per NFR-02).
        public string ReceiptFileUrl { get; set; }

        public TopUpRequestStatus Status { get; set; }
        public string? RejectionReason { get; set; }

        public string? VerifiedBy { get; set; }
        public User? VerifiedByUser { get; set; }
        public DateTime? VerifiedOn { get; set; }
    }
}
