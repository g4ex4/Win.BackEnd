using Domain.Responses;
using MediatR;

namespace Application.Videos.Commands.DeleteCommands
{
    public class DeleteVideoCommand : IRequest<Response>
    {
        public int VideoId { get; set; }
    }
}
