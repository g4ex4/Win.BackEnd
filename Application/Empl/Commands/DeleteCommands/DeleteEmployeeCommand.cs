using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommand : IRequest<Response>
    {
        [Required(ErrorMessage = "EmployeeId is required.")]
        public int EmployeeId { get; set; }
    }
}
