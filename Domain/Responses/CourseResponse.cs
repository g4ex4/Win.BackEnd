using Domain.Entities;

namespace Domain.Responses
{
    public class CourseResponse : EntityResponse<int>
    {

        public CourseResponse(Course course, int statusCode, string message, bool isSuccess) 
            : base(course, statusCode, message, isSuccess)
        {
            Id = course?.Id ?? 0;
            CategoryId = course?.CategoryId ?? 0;
            ImageCourseName = course?.ImageCourseName ?? "";
            ImageCourseUrl = course?.ImageCourseUrl ?? "";
            Title = course?.Title ?? "";
            Description = course?.Description ?? "";

        }
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string ImageCourseName { get; set; }
        public string ImageCourseUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
    }
}
