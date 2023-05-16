using Domain.Responses;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Win.WebApi.Requests
{
    public class RegisterEmployeeRequest : IRequest<Response>
    {
        [Required]
        public string UserName { get; set; }
        [Required]
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
            return JsonSerializer.Serialize(this);
        }
    }
}
