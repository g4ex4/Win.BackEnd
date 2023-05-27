using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Domain.Responses;
using MediatR;

namespace Application.Students.Commands.UpdateCommands
{
    public class StudentResetPasswordCommand : IRequest<Response>
    {
        [Required]
        public string Email { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
