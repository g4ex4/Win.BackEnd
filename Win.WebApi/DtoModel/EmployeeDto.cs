using System.ComponentModel.DataAnnotations;

namespace Win.WebApi.DtoModel
{
    public class EmployeeDto
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
    }
}
