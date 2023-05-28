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

namespace Application.Students.Commands.CreateCommands
{
    public class AuthorizeStudentHandler : IRequestHandler<AuthorizeStudentCommand, Response>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthorizeStudentHandler(IStudentDbContext dbContext, IOptions<JwtSettings> jwtSettings)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<Response> Handle(AuthorizeStudentCommand command, CancellationToken cancellationToken)
        {

            var student = await _dbContext.Students.FirstOrDefaultAsync(e => e.UserName == command.UserName);
            if (student == null || !BCryptNet.BCrypt.Verify(command.PasswordHash, student.PasswordHash))
            {
                return new Response(401, "Unauthorized", false);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),

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
