using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.Models
{
    public class PageCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
