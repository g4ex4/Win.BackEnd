using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class ConfirmEmailCommand : IRequest<Response>
    {
        public string Email { get; set; }
    }
}
