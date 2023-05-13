using Domain.Entities;

namespace Domain.Responses
{
    public class CourseResponse : EntityResponse<int>
    {

        public CourseResponse(Course course, int statusCode, string message, bool isSuccess) : base(course, statusCode, message, isSuccess)
        {
            Title = course.Title;
            Description = course.Description;
            MentorId = course.MentorId;

        }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MentorId { get; set; }
    }
}
