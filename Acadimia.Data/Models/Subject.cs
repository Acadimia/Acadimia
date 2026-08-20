namespace Acadimia.Data.Models
{
    // Academic subject taught (e.g. Math, Physics), so Groups/Courses can be
    // scoped by subject in addition to Grade (class level).
    public class Subject : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
