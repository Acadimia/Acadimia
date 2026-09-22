namespace Acadimia.Data.Enums
{
    public enum RescheduleRequestStatus
    {
        Pending = 1,
        Approved = 2,
        Rejected = 3,
        Cancelled = 4   // the booking itself was cancelled while the request was pending
    }

    public enum BookingPaymentStatus
    {
        Unpaid = 1,
        Paid = 2,
        Refunded = 3
    }
}
