using Application.Interfaces;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ChangePasswordEmployeeCommandHandler : IRequestHandler<ChangePasswordEmployeeCommand, PersonResponse>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;
        private readonly IPasswordHasher<Employee> _passwordHasher;

        public ChangePasswordEmployeeCommandHandler(IEmployeeDbContext dbContext, EmailService emailService, IPasswordHasher<Employee> passwordHasher)
        {
            _dbContext = dbContext;
            _emailService = emailService;
            _passwordHasher = passwordHasher;
        }

        public async Task<PersonResponse> Handle(ChangePasswordEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Email == command.Email, cancellationToken);
            if (entity == null || _passwordHasher.VerifyHashedPassword(entity, entity.PasswordHash, command.OldPassword) != PasswordVerificationResult.Success)
            {
                return new PersonResponse(401, "User is not found or password is incorrect", false, null);
            }

            string hashedNewPassword = _passwordHasher.HashPassword(entity, command.NewPassword);
            entity.PasswordHash = hashedNewPassword;

            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailInfoAsync(command.Email);

            var person = new Person
            {
                Id = entity.Id,
                UserName = entity.UserName
            };

            return new PersonResponse(200, "Password changed successfully", true, person);
        }
    }
}
