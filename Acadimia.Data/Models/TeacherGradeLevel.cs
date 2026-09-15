namespace Acadimia.Data.Models
{
   
    public class TeacherGradeLevel
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}