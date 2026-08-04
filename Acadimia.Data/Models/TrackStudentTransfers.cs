using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class TrackStudentTransfers : BaseModel
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

        


    }
}
