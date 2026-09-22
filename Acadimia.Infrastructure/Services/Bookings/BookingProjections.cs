using Acadimia.Data.Models;
using Acadimia.Infrastructure.Dtos.Bookings;
using System.Linq.Expressions;

namespace Acadimia.Infrastructure.Services.Bookings
{
    // Booking -> BookingDto as an *expression* so EF translates it to SQL and loads
    // Teacher/User/Student/Subject itself. (The old static ToDto(b) method was
    // evaluated in memory on entities without Includes, so b.Teacher was null.)
    public static class BookingProjections
    {
        public static readonly Expression<Func<Booking, BookingDto>> ToDto = b => new BookingDto
        {
            Id = b.Id,
            TeacherId = b.TeacherId,
            TeacherName = b.Teacher.User != null ? b.Teacher.User.Name : null,
            StudentId = b.StudentId,
            StudentName = b.Student != null ? b.Student.Name : null,
            SubjectId = b.SubjectId,
            SubjectName = b.Subject != null ? b.Subject.Name : null,
            TeachingMode = b.TeachingMode,
            Date = b.Date,
            StartTime = b.StartTime,
            DurationMinutes = b.DurationMinutes,
            Price = b.Price,
            Status = b.Status,
            StudentNote = b.StudentNote,
            RejectionReason = b.RejectionReason,
            PaidOn = b.PaidOn,
            CancellationReason = b.CancellationReason,
            CreatedOn = b.CreatedOn
        };
    }
}
