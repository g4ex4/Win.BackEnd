using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.Empl.Commands.UpdateCommands
{
    public class UpdateEmployeeCommand : IRequest<Response>
    {
        [Required(ErrorMessage = "Id is required.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Job title is required.")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Experience is required.")]
        public string Experience { get; set; }

        [Required(ErrorMessage = "Education is required.")]
        public string Education { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
