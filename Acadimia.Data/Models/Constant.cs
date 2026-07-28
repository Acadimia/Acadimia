using Acadimia.Data.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Data.Models
{
    public class Constant 
    {
        [Display(Name = "Id", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }
        public string? Comment { get; set; }
        public string? Icon { get; set; }
		public int? ParentId { get; set; }
        public Constant? Parent { get; set; }
        
        // For Search Operations.
        [NotMapped]
		public string? Keyword { get; set; }
	}
}
