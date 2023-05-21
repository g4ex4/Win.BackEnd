using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeHandler : IRequestHandler<CreateEmployeeCommand, Response>
    {
        //private readonly UserManager<Employee> _userManager;
        private readonly IEmployeeDbContext _dbContext;

        public RegisterEmployeeHandler(
            //UserManager<Employee> userManager,
            IEmployeeDbContext employeeDbContext
            )
        {
            //_userManager = userManager;
            _dbContext = employeeDbContext;
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
            return new Response(200, "Employee added successfully", true);
            
        }

        
    }
}
