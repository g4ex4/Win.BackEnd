using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommand : IRequest<PersonResponse>
    {
        public int EmployeeId { get; set; }
    }
}
