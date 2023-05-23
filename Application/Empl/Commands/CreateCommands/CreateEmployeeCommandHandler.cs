using Application.Common.Exceptions;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly EmailService _emailService;

        public RegisterEmployeeHandler(IEmployeeDbContext employeeDbContext, EmailService emailService            )
        {
            _dbContext = employeeDbContext;
            _emailService = emailService;
        }
        public async Task<Response> Handle(CreateEmployeeCommand command,
            CancellationToken cancellationToken)
        {

            Employee emp = new Employee
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = command.PasswordHash,
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
