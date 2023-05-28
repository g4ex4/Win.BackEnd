using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteStudentCommand : IRequest<Response>
    {
        public int StudentId { get; set; }
    }
}
