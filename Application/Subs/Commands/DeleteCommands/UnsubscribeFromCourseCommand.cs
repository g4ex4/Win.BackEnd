using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Subs.Commands.DeleteCommands
{
    public class UnsubscribeFromCourseCommand : IRequest<Response>
    {
        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "CourseId is required.")]
        public int CourseId { get; set; }
    }
}
