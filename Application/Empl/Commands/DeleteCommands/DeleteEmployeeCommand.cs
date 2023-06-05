using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommand : IRequest<Response>
    {
        public int EmployeeId { get; set; }
    }
}
