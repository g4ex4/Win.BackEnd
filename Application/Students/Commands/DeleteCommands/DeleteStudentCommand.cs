using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteStudentCommand : IRequest<Response>
    {
        [Required(ErrorMessage = "StudentId is required.")]
        public int StudentId { get; set; }
    }
}
