using Application.Interfaces;
using Application.JWT;
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
using BCryptNet = BCrypt.Net;

namespace Application.Empl.Commands.CreateCommands
{
    public class AuthorizeEmployeeHandler : IRequestHandler<AuthorizeEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public AuthorizeEmployeeHandler(IEmployeeDbContext dbContext, IOptions<JwtSettings> jwtSettings, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<EmployeeResponse> Handle(AuthorizeEmployeeCommand command, CancellationToken cancellationToken)
        {
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Email == command.Email);
            if (employee == null || _passwordHasher
                .VerifyHashedPassword(null, employee.PasswordHash, command.PasswordHash) != PasswordVerificationResult.Success)
            {
                return new EmployeeResponse(401, "Wrong login or password", false, null, null);
            }

            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
        new Claim(ClaimTypes.Role, employee.RoleId.ToString()),
    };

            var token = GenerateJwtToken(claims);
            return new EmployeeResponse(200, "Authorized", true, token, employee);
            //{
            //    JwtToken = token
            //};
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
