using Application.Interfaces;
using Application.JWT;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Employee = Domain.Entities.Employee;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeHandler : IRequestHandler<RegisterEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public RegisterEmployeeHandler(IEmployeeDbContext dbContext, EmailService emailService, IOptions<JwtSettings> jwtSettings, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<EmployeeResponse> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
        {
            var isEmailExists = await _dbContext.Employees.AnyAsync(employee => employee.Email == command.Email, cancellationToken);
            if (isEmailExists)
            {
                return new EmployeeResponse(400, "The mail already exists.", false, null);
            }

            string hashedPassword = _passwordHasher.HashPassword(null, command.PasswordHash);

            Employee emp = new Employee
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = hashedPassword,
                JobTitle = command.JobTitle,
                Experience = command.Experience,
                Education = command.Education,
                RoleId = 2,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow
            };

            await _dbContext.Employees.AddAsync(emp);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailAsync(command.Email);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, emp.Id.ToString()),
            new Claim(ClaimTypes.Role, emp.RoleId.ToString())
        };

            var token = GenerateJwtToken(claims);

            return new EmployeeResponse(200, "Employee added successfully", true, emp)
            {
                JwtToken = token
            };
        }

        private string GenerateJwtToken(IEnumerable<Claim> claims)
        {
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(7);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
