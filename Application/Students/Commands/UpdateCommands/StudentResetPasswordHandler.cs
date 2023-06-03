using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace Application.Students.Commands.UpdateCommands
{
    public class StudentResetPasswordHandler : IRequestHandler<StudentResetPasswordCommand, PersonResponse>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly IPasswordHasher<Student> _passwordHasher;

        public StudentResetPasswordHandler(IStudentDbContext dbContext, EmailService emailService, IPasswordHasher<Student> passwordHasher)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<PersonResponse> Handle(StudentResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Students.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user != null)
            {
                const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_-+=<>?";

                StringBuilder sb = new StringBuilder();
                Random rnd = new Random();

                for (int i = 0; i < 8; i++)
                {
                    int index = rnd.Next(chars.Length);
                    sb.Append(chars[index]);
                }
                string hashedPassword = _passwordHasher.HashPassword(user, sb.ToString());
                user.PasswordHash = hashedPassword;
                await _dbContext.SaveChangesAsync(cancellationToken);
                await _emailService.SendEmailResetPasswordAsync(request.Email, sb.ToString());

                return new PersonResponse(200, "A temporary password has been sent to your email", true, user);
            }
            return new PersonResponse(400, "User not found", true, null);
        }
    }
}
