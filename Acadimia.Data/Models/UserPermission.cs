using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.Models
{
    public class UserPermission
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int UserTypeId { get; set; }
        [Required]
        public UserType? UserType { get; set; }
        [Required]
        public int PageId { get; set; }
        public Page? Page { get; set; }
    }
}
