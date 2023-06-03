using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeCommand : IRequest<PersonResponse>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Experience { get; set; }
        [Required]
        public string Education { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(new
            {
                UserName,
                Email,
                PasswordHash,
                JobTitle,
                Experience,
                Education
            });
        }
    }
}
