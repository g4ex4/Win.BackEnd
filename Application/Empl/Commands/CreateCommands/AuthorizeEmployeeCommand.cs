using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class AuthorizeEmployeeCommand : IRequest<Response>
    {
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
