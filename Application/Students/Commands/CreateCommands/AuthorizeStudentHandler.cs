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

namespace Application.Students.Commands.CreateCommands
{
    public class AuthorizeStudentHandler : IRequestHandler<AuthorizeStudentCommand, StudentResponse>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<Student> _passwordHasher;

        public AuthorizeStudentHandler(IStudentDbContext dbContext, IOptions<JwtSettings> jwtSettings, IPasswordHasher<Student> passwordHasher)
        {
            _dbContext = dbContext;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<StudentResponse> Handle(AuthorizeStudentCommand command, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students.FirstOrDefaultAsync(e => e.Email == command.Email);
            if (student == null || _passwordHasher.VerifyHashedPassword(null, student.PasswordHash, command.PasswordHash) != PasswordVerificationResult.Success)
            {
                return new StudentResponse(401, "Unauthorized", false, null, null);
            }

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, student.Id.ToString())
        };

            var token = GenerateJwtToken(claims);
            return new StudentResponse(200, "Authorized", true, token, student);
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
