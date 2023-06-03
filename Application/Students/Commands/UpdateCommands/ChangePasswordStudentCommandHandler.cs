using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ChangePasswordStudentCommandHandler : IRequestHandler<ChangePasswordStudentCommand, PersonResponse>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly IPasswordHasher<Student> _passwordHasher;

        public ChangePasswordStudentCommandHandler(IStudentDbContext dbContext, EmailService emailService, IPasswordHasher<Student> passwordHasher)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<PersonResponse> Handle(ChangePasswordStudentCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Students.FirstOrDefaultAsync(c => c.Email == command.Email, cancellationToken);
            if (entity == null || _passwordHasher.VerifyHashedPassword(entity, entity.PasswordHash, command.OldPassword) != PasswordVerificationResult.Success)
            {
                return new PersonResponse(401, "User not found or incorrect password", false, null);
            }

            string hashedNewPassword = _passwordHasher.HashPassword(entity, command.NewPassword);
            entity.PasswordHash = hashedNewPassword;
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailInfoAsync(command.Email);

            return new PersonResponse(200, "Password changed successfully", true, entity);
        }
    }
}
