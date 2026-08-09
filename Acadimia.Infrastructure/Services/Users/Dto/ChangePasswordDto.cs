using Acadimia.Data.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acadimia.Infrastructure.Services.Users.Dto
{
    public class ChangePasswordDto
    {
        [Display(Name = "CurrentPassword", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string CurrentPassword { get; set; }

        [Display(Name = "NewPassword", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public string NewPassword { get; set; }

        [Display(Name = "ConfirmPassword", ResourceType = typeof(Messages))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        [Compare("NewPassword", ErrorMessageResourceName = "ComparePassword", ErrorMessageResourceType = typeof(Messages))]
        public string ConfirmPassword { get; set; }
    }
}
