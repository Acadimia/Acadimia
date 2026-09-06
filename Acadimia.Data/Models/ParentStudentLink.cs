namespace Acadimia.Data.Models
{
   
    public class ParentStudentLink : BaseModel
    {
        public int Id { get; set; }

        public string ParentUserId { get; set; }
        public User ParentUser { get; set; }

        public int StudentId { get; set; }
        public Student Student { get; set; }
        public int? RelationTypeId { get; set; }
        public Constant RelationType { get; set; }
        public bool IsPrimaryContact { get; set; }
    }
}