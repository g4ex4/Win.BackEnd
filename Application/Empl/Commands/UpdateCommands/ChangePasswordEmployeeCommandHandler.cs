using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ChangePasswordEmployeeCommandHandler : IRequestHandler<ChangePasswordEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public ChangePasswordEmployeeCommandHandler(IEmployeeDbContext userManager, EmailService emailService, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = userManager;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<Response> Handle(ChangePasswordEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Email == command.Email, cancellationToken);
            if (entity == null || _passwordHasher.VerifyHashedPassword(entity, entity.PasswordHash, command.OldPassword) != PasswordVerificationResult.Success)
            {
                return new Response(401, "User is not found or password is incorrect", false);
            }

            string hashedNewPassword = _passwordHasher.HashPassword(entity, command.NewPassword);
            entity.PasswordHash = hashedNewPassword;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailInfoAsync(command.Email);

            return new Response(200, "Password changed successfully", true);
        }
    }
}
