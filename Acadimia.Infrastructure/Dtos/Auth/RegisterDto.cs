using Acadimia.Data.Models;
using Acadimia.Data.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Acadimia.Infrastructure.Dtos.Auth
{
    public class RegisterDto
    {
        [Display(Name = "Name", ResourceType = typeof(Messages))]
        [StringLength(ApplicationConstant.MaxStringName, MinimumLength = ApplicationConstant.MinStringName,
            ErrorMessageResourceName = "StringLengthValidation", ErrorMessageResourceType = typeof(Messages))]
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

        [Display(Name = "Password", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string Password { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        [Compare("Password", ErrorMessageResourceName = "ComparePassword", ErrorMessageResourceType = typeof(Messages))]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int GenderId { get; set; }

        public int? UserTypeId { get; set; }
    }
}