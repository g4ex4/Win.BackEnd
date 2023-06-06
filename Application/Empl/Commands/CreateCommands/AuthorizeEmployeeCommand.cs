using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Empl.Commands.CreateCommands
{
    public class AuthorizeEmployeeCommand : IRequest<EmployeeResponse>
    {
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string PasswordHash { get; set; }
    }
}
