using Domain.Responses;
using MediatR;

namespace Application.Subs.Commands.CreateCommands
{
    public class SubscribeToCourseCommand : IRequest<SubscriptionResponse>
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
    }
}
