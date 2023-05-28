using Domain.Responses;
using MediatR;

namespace Application.Subs.Commands.DeleteCommands
{
    public class UnsubscribeFromCourseCommand : IRequest<Response>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
