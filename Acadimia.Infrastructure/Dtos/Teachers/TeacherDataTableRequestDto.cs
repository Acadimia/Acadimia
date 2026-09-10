using Acadimia.Infrastructure.Dtos;

namespace Acadimia.Infrastructure.Dtos.Teachers
{
    public class TeacherDataTableRequestDto : DataTableRequestDto
    {
        public int? GradeId { get; set; }
    }
}