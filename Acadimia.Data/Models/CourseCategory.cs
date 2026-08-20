namespace Acadimia.Data.Models
{
    // Classification for pre-recorded online courses (e.g. Exam Prep, Enrichment).
    public class CourseCategory : BaseModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
