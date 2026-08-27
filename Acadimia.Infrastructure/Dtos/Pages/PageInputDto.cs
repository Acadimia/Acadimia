using Acadimia.Data.Resources;
using System.ComponentModel.DataAnnotations;

namespace Acadimia.Infrastructure.Dtos.Pages
{
    public class PageInputDto
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(250, MinimumLength = 3, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Display(Name = "NameEn", ResourceType = typeof(Messages))]
        [StringLength(250, MinimumLength = 3, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string NameEn { get; set; }

        public string? Link { get; set; }
        public string? Icon { get; set; }
        public bool InMenu { get; set; }
        public bool IsActive { get; set; }
        public bool IsAjax { get; set; }

        // Just the FK ids — no Parent/Module/Category navigation objects
        public int? ParentId { get; set; }
        public int? ModuleId { get; set; }

        [Display(Name = "Category", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int? CategoryId { get; set; }
    }
}