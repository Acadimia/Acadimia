namespace Acadimia.Data.Models
{
   
    public class TeacherRating : BaseModel
    {
        public int Id { get; set; }

        public int BookingId { get; set; }
        public Booking Booking { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public string StudentId { get; set; }
        public User Student { get; set; }

        public int RatingValue { get; set; } 
        public string? Review { get; set; }
    }
}