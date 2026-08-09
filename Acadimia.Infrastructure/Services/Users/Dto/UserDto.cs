using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.Users.Dto
{
	public class UserDto
	{
        public string? Id { get; set; }

        [Display(Name = "Password", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string? Password { get; set; }
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        [Compare("Password", ErrorMessageResourceName = "ComparePassword", ErrorMessageResourceType = typeof(Messages))]
        public string? ConfirmPassword { get; set; }

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
        [Phone(ErrorMessageResourceName = "InvalidPhone", ErrorMessageResourceType = typeof(Messages))]
        public string PhoneNumber { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int? GenderId { get; set; }
        public Constant? Gender { get; set; }


        [Display(Name = "UserType", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int? UserTypeId { get; set; }
        public virtual UserType? UserType { get; set; }
        public bool IsActive { get; set; }
        public string? Avatar { get; set; }
        public List<UserType>? UserTypes { get; set; }
        public List<Constant>? Genders { get; set; }


    }
}
