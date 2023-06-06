using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Students.Commands.CreateCommands
{
    public class AuthorizeStudentCommand : IRequest<StudentResponse>
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }
    }
}
