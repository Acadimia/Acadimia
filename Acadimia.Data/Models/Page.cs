using Acadimia.Data.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Acadimia.Data.Models
{
    public class Page
    {
        [Display(Name = "Id", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Display(Name = "NameEn", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string NameEn { get; set; }

        public string? Link { get; set; }

        public string? Icon { get; set; }
        public bool InMenu { get; set; }
        public int? ParentId { get; set; }
        public Page? Parent { get; set; }
        public bool IsActive { get; set; }
        public bool IsAjax { get; set; }

        public bool IsDeleted { get; set; } = false;

        public int? ModuleId { get; set; }
        public Module? Module { get; set; }
        
        [Display(Name = "Category", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int? CategoryId { get; set; }
        public PageCategory? Category { get; set; }
        
        
        [NotMapped] // for search operations
        public string? Keyword { get; set; }
        [NotMapped] // For Search by Status.
        public bool? IsActiveSearch { get; set; }


    }
}
