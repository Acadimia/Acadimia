using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class Teacher : BaseModel
    {
        public int Id { get; set; }

        public int GradeId { get; set; }

        public Grade Grade { get; set; }
    }
}
