using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ResetPasswordCommand : IRequest<Response>
    {
        [Required]
        public string Email { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
