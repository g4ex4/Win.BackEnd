using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class RegisterEmployeeHandler : IRequestHandler<CreateEmplCommand, Response>
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
        public async Task<Response> Handle(CreateEmplCommand command,
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
            return new Response(200, "Empl added successfully", true);
            //return new Response(200, "Initial user created", true);
        }

        //private void SetUserProperties(Employee user, string UserName, string email)
        //{
        //    user.UserName = UserName;
        //    user.Email = email;
        //    user.UserName = email;
        //}

        //private async Task<IdentityResult> RegisterUser(CreateEmplCommand request)
        //{
        //    var Employee = new Employee();
        //    SetUserProperties(Employee, request.UserName, request.Email);

        //    var result = await _userManager.CreateAsync(Employee, request.PasswordHash);
        //    return result;
        //}
    }
}
