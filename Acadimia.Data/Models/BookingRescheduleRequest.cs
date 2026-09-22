using Acadimia.Data.Enums;

namespace Acadimia.Data.Models
{
    // FR-S15: a student's request to move a confirmed tutoring session to a new
    // date/time. It never changes the booking by itself - the instructor decides.
    public class BookingRescheduleRequest : BaseModel
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        // Snapshot of the schedule at the time of the request.
        public DateTime OriginalDate { get; set; }
        public TimeSpan OriginalStartTime { get; set; }

        public DateTime ProposedDate { get; set; }
        public TimeSpan ProposedStartTime { get; set; }
        public string? Note { get; set; }

        public RescheduleRequestStatus Status { get; set; }

        // Instructor's user id (audit only, no FK).
        public string? DecisionBy { get; set; }
        public DateTime? DecisionOn { get; set; }
        public string? RejectionReason { get; set; }
    }
}
