using System.ComponentModel.DataAnnotations;
using Acadimia.Data.Resources;

namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherInputDto
    {
        public int Id { get; set; }

        [Display(Name = "Grade")] 
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(Messages))]
        public int GradeId { get; set; }
    }
}