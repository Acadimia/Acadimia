using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class groups : BaseModel
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public int gradeId { get; set; }
        public int teacherId { get; set; }

    }
}
