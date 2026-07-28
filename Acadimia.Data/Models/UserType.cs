using Acadimia.Data.Resources;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Acadimia.Data.Models
{
    [Index(nameof(Name), IsUnique = true, Name = "IX_UserTypes_UniqueName")]
    public class UserType : BaseModel
    {
        [Required]
        public int Id { get; set; }

        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }
        List<UserPermission>? Permissions { get; set; }
    }
}
