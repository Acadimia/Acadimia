using Acadimia.Data.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Acadimia.Data.Models
{
    // Instructor request to withdraw earnings to their own bank account.
    // Creates one WalletTransaction row on submit (Direction=Out, Type=Withdrawal,
    // Status=Pending); Admin/Finance approval flips it to Accepted (amount reserved,
    // not yet debited); confirming the bank transfer flips it to Completed and debits
    // Wallet.Balance; rejection at any pre-completion stage flips it to Rejected.
    public class WithdrawalRequest : BaseModel
    {
        public int Id { get; set; }

        public string InstructorId { get; set; }
        public User Instructor { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        // Destination bank account details (access-restricted per NFR-02).
        public string BankIBAN { get; set; }
        public string BankName { get; set; }
        public string AccountHolderName { get; set; }

        public WithdrawalRequestStatus Status { get; set; }

        public string? ApprovedBy { get; set; }
        public User? ApprovedByUser { get; set; }

        // Bank confirmation reference once the transfer is completed.
        public string? TransferReference { get; set; }
        public string? RejectionReason { get; set; }
    }
}
