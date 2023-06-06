using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Subs.Commands.CreateCommands
{
    public class SubscribeToCourseCommand : IRequest<SubscriptionResponse>
    {
        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }

        [Required(ErrorMessage = "CourseId is required.")]
        public int CourseId { get; set; }
    }
}
