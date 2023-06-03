using Application.Interfaces;
using Domain.Common;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommandHandler : IRequestHandler<DeleteEmplCommand, PersonResponse>
    {
        private readonly IEmployeeDbContext _dbContext;

        public DeleteEmplCommandHandler(IEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonResponse> Handle(DeleteEmplCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
            if (entity == null)
            {
                return new PersonResponse(400, "Employee not found", false, null);
            }

            _dbContext.Employees.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var person = new Person
            {
                Id = entity.Id,
                UserName = entity.UserName
            };

            return new PersonResponse(200, "Employee deleted successfully", true, person);
        }
    }
}
