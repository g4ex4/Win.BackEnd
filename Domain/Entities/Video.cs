using Domain.Common;

namespace Domain.Entities
{
    public class Video : BaseEntity<int>
    {
        public string VideoName { get; set; }
        public string Url { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
