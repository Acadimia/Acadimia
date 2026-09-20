using Acadimia.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class Student : BaseModel
    {
        public int Id { get; set; }

        // Links the academic Student record to the authenticated Identity account.
        // Required by the student dashboard to enforce ownership of academic data.
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string Name { get; set; }              
        public int FatherId { get; set; }
        public Father Father { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
        public string WhatsAppNumber { get; set; }

        public string? Location { get; set; }

    }
}
