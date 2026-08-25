using Acadimia.Core.Enums;

namespace Acadimia.Data.Models
{
    // Uploaded file (document, video, slides) attached to a Lesson.
    public class LessonMaterial : BaseModel
    {
        public int Id { get; set; }

        public int LessonId { get; set; }
        public Lesson Lesson { get; set; }

        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public LessonMaterialFileType FileType { get; set; }

        public string UploadedBy { get; set; }
        public User UploadedByUser { get; set; }

        public bool IsArchived { get; set; }
    }
}
