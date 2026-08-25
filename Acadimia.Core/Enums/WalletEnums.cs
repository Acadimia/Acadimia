namespace Acadimia.Core.Enums
{
    // Which side of the wallet a WalletTransaction row shows up on:
    // In  = money received into the wallet (e.g. TopUp, InstructorCredit)
    // Out = money sent out of the wallet   (e.g. Withdrawal, EnrollmentDeduction)
    public enum WalletTransactionDirection
    {
        In = 1,
        Out = 2
    }

    public enum WalletTransactionType
    {
        TopUp = 1,
        Withdrawal = 2,
        EnrollmentDeduction = 3,
        InstructorCredit = 4
    }

    // Lifecycle of a single WalletTransaction row. TopUp/Withdrawal start at Pending
    // and move to Accepted/Rejected (and, for withdrawals, on to Completed) as
    // Admin/Finance acts on them. EnrollmentDeduction/InstructorCredit are written
    // directly as Completed. Reversed is an exception path for admin corrections.
    public enum WalletTransactionStatus
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Completed = 4,
        Reversed = 5
    }

    public enum TopUpRequestStatus
    {
        PendingVerification = 1,
        Completed = 2,
        Rejected = 3
    }

    public enum WithdrawalRequestStatus
    {
        PendingApproval = 1,
        ApprovedPendingTransfer = 2,
        Completed = 3,
        Rejected = 4
    }
}