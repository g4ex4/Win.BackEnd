using Application.Interfaces;
using Application.Services;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Employee = Domain.Entities.Employee;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeHandler : IRequestHandler<RegisterEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;

        public RegisterEmployeeHandler(IEmployeeDbContext dbContext, EmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task<Response> Handle(RegisterEmployeeCommand command, CancellationToken cancellationToken)
        {
            var isEmailExists = await _dbContext.Employees
                .AnyAsync(employee => employee.Email == command.Email, cancellationToken);
            if (isEmailExists)
            {
                return new Response(400, "The mail already exists.", false);
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(command.PasswordHash);
            Employee emp = new Employee
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = hashedPassword,
                JobTitle = command.JobTitle,
                Experience = command.Experience,
                Education = command.Education,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };

            await _dbContext.Employees.AddAsync(emp);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _emailService.SendEmailAsync(command.Email);

            return new Response(200, "Employee added successfully", true);
        }
    }
}
