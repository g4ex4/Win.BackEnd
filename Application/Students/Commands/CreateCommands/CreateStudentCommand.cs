using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.Empl.Commands.CreateCommands
{
    public class CreateStudentCommand : IRequest<StudentResponse>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
