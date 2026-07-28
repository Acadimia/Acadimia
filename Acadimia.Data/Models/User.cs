using Acadimia.Data.Resources;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Acadimia.Data.Models
{
    [Index(nameof(Email), IsUnique = true, Name ="IX_Users_UniqueEmail")]
    [Index(nameof(PhoneNumber), IsUnique = true, Name = "IX_Users_UniquePhoneNo")]
    public class User : IdentityUser
    {
        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName, ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Name { get; set; }

        [Display(Name = "Email", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        [EmailAddress(ErrorMessageResourceName = "InvalidEmail", ErrorMessageResourceType = typeof(Messages))]
        public string Email { get; set; }

        [Display(Name = "PhoneNumber", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        [Phone(ErrorMessageResourceName = "InvalidNumber", ErrorMessageResourceType = typeof(Messages))]
        public string PhoneNumber { get; set; }
        
        [Display(Name = "Gender", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int? GenderId { get; set; }
        public Constant? Gender { get; set; }


        [Display(Name = "UserType", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int UserTypeId { get; set; }
        public virtual UserType? UserType { get; set; }

        public bool IsActive { get; set; }
        public string? Avatar { get; set; }

        [Required]
        public bool IsDeleted { get; set; } = false;
        public string? DeletedBy { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? UpdatedBy { get; set; }

        //For Search Operations.
        [NotMapped]
        public string? Keyword { get; set; }
        [NotMapped] // For Search by Status.
        public bool? IsActiveSearch { get; set; }
    }
}
