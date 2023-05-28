using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Title { get; set; }  
        public string Description { get; set; }
        public int MentorId { get; set; }
        public Employee Mentor { get; set; }
        public List<Video> Videous { get; set; }
        public List<StudentCourse> StudentCourse { get; set; }
        public List<CategoryCourse> CategoryCourse { get; set; }
        public List<CourseSubscription> CourseSubscription { get; set; }
    }
}
