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

namespace Application.Empl.Commands.CreateCommands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly JwtSettings _jwtSettings;
        private readonly IPasswordHasher<Student> _passwordHasher;

        public CreateStudentCommandHandler(IStudentDbContext dbContext, EmailService emailService,
            IOptions<JwtSettings> jwtSettings, IPasswordHasher<Student> passwordHasher)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _jwtSettings = jwtSettings.Value;
            _passwordHasher = passwordHasher;
        }

        public async Task<Response> Handle(CreateStudentCommand command, CancellationToken cancellationToken)
        {
            var isEmailExists = await _dbContext.Students.AnyAsync(student => student.Email == command.Email, cancellationToken);
            if (isEmailExists)
            {
                return new Response(400, "The mail already exists.", false);
            }

            string hashedPassword = _passwordHasher.HashPassword(null, command.PasswordHash);
            Student student = new Student
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = hashedPassword,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };

            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendStudentEmailAsync(command.Email);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, student.Id.ToString()),

            };

            var token = GenerateJwtToken(claims);

            return new Response(200, "Student added successfully", true, token);
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
