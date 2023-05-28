using Domain.Responses;
using MediatR;

namespace Application.Students.Commands.CreateCommands
{
    public class StudentConfirmEmailCommand : IRequest<Response>
    {
        public string Email { get; set; }
    }
}
