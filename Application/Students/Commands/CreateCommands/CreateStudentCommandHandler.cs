using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Empl.Commands.CreateCommands
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, Response>
    {
        //private readonly UserManager<Employee> _userManager;
        private readonly IStudentDbContext _dbContext;

        public CreateStudentCommandHandler(
            //UserManager<Employee> userManager,
            IStudentDbContext employeeDbContext
            )
        {
            //_userManager = userManager;
            _dbContext = employeeDbContext;
        }
        public async Task<Response> Handle(CreateStudentCommand command,
            CancellationToken cancellationToken)
        {

            Student student = new Student
            {
                UserName = command.UserName,
                Email = command.Email,
                PasswordHash = command.PasswordHash,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new Response(200, "Student added successfully", true);
            
        }

        
    }
}
