namespace Acadimia.Data.Models
{
    // A lesson/session belonging to a Group (live) or Course (pre-recorded) -
    // the organizing unit for LessonMaterials.
    public class Lesson : BaseModel
    {
        public int Id { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public string Title { get; set; }
        public int OrderIndex { get; set; }
    }
}
