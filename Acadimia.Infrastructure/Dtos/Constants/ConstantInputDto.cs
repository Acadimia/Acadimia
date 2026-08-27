using System.ComponentModel.DataAnnotations;
using Acadimia.Data.Models;
using Acadimia.Data.Resources;

namespace Acadimia.Infrastructure.Dtos.Constants
{
  
    public class ConstantInputDto
    {
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName,
            ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        public string? Comment { get; set; }
        public string? Icon { get; set; }
        public int? ParentId { get; set; }
    }
}