using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using BCryptNet = BCrypt.Net;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ChangePasswordEmployeeCommandHandler :IRequestHandler<ChangePasswordEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;

        public ChangePasswordEmployeeCommandHandler(IEmployeeDbContext userManager, EmailService emailService)
        {
            _dbContext = userManager;
            _emailService = emailService;
        }

        public async Task<Response> Handle(ChangePasswordEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Email == command.Email, cancellationToken);
            if (entity == null || !BCryptNet.BCrypt.Verify(command.OldPassword, entity.PasswordHash))
            {
                return new Response(401, "User in not found", false);
            }
            
            string hashedNewPassword = BCrypt.Net.BCrypt.HashPassword(command.NewPassword);
            entity.PasswordHash = hashedNewPassword;
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailInfoAsync(command.Email);

            return new Response(200, "Password changed successfully", true);

        }
    }
}
