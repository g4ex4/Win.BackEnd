using Application.Interfaces;
using Application.JWT;
using Application.Services;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Options;
using Employee = Domain.Entities.Employee;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeHandler : IRequestHandler<RegisterEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly JwtSettings _jwtSettings;

        public RegisterEmployeeHandler(IEmployeeDbContext dbContext, EmailService emailService, IOptions<JwtSettings> jwtSettings)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Response> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.PasswordHash);
            Employee emp = new Employee
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = hashedPassword,
                JobTitle = command.JobTitle,
                Experience = command.Experience,
                Education = command.Education,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };

            await _dbContext.Employees.AddAsync(emp);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailAsync(command.Email);

            // Генерация и сохранение токена JWT
            //var token = GenerateJwtToken(emp);

            return new Response(200, "Employee added successfully", true);
        }

        //private string GenerateJwtToken(Employee employee)
        //{
        //    var claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
        //    // Добавьте другие требуемые утверждения (claims) для токена JWT

        //};
            
        //    var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
        //    var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
        //    var expires = DateTime.UtcNow.AddDays(7);

        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(claims),
        //        Expires = expires,
        //        SigningCredentials = credentials,
        //        Issuer = _jwtSettings.Issuer,
        //        Audience = _jwtSettings.Audience
        //    };

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    return tokenHandler.WriteToken(token);
        //}
    }
}
