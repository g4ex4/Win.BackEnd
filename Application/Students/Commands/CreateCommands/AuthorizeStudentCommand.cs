using Domain.Responses;
using MediatR;

namespace Application.Students.Commands.CreateCommands
{
    public class AuthorizeStudentCommand : IRequest<Response>
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
    }
}
