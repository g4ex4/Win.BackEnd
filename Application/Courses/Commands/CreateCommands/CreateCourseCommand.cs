using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Courses.Commands.CreateCommands
{
    public class CreateCourseCommand : IRequest<CourseResponse>
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int MentorId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
