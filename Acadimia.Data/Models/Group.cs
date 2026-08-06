using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class Group : BaseModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int GradeId { get; set; }
        public Grade Grade { get; set; }

        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

    }
}
