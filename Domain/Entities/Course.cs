using Domain.Common;
using Domain.Links;

namespace Domain.Entities
{
    public class Course : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageCourseName { get; set; }
        public string ImageCourseUrl { get; set; }
        public int CategoryId { get; set; }
        public List<Video> Videous { get; set; }
        public List<CourseSubscription> CourseSubscriptions { get; set; }
        public List<UserCourse> UserCourses { get; set; }
        public List<CategoryCourse> CategoryCourses { get; set; }
        
        
    }
}
