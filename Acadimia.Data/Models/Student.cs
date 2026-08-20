using Acadimia.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Acadimia.Data.Models
{
    public class Student : BaseModel
    {
        public int Id { get; set; }

        public int FatherId { get; set; }

        public Father Father { get; set; }

        public int Grade_id { get; set; }

        public Grade Grade { get; set; }

        public string WhatsAppNumber { get; set; }

    }
}
