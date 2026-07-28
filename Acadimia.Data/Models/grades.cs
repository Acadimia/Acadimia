using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class grades : BaseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Section { get; set; }

    }
}
