using Application.Interfaces;
using Application.JWT;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCryptNet = BCrypt.Net;

namespace Application.Empl.Commands.CreateCommands
{
    public class AuthorizeEmployeeHandler : IRequestHandler<AuthorizeEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthorizeEmployeeHandler(IEmployeeDbContext dbContext, IOptions<JwtSettings> jwtSettings)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Response> Handle(AuthorizeEmployeeCommand command, CancellationToken cancellationToken)
        {
            
            var employee = await _dbContext.Employees.FirstOrDefaultAsync(e => e.UserName == command.UserName);
            if (employee == null || !BCryptNet.BCrypt.Verify(command.PasswordHash, employee.PasswordHash))
            {
                return new Response(401, "Unauthorized", false);
            }
    
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, employee.Id.ToString()),
                new Claim(ClaimTypes.Role, employee.RoleId.ToString()),
                
            
            };

            var token = GenerateJwtToken(claims);
            return new Response(200, "Authorized", true, token);
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
