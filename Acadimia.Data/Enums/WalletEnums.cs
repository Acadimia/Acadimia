namespace Acadimia.Data.Enums
{
   
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
        InstructorCredit = 4,
        BookingRefund = 5,           
        BookingRefundReversal = 6     

    }

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