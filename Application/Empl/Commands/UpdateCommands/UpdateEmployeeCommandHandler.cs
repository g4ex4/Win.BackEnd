using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.Empl.Commands.UpdateCommands
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;

        public UpdateEmployeeCommandHandler(IEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            if (entity == null)
            {
                return new Response(401, "Employee not found", false);
            }

            entity.JobTitle = command.JobTitle;
            entity.Experience = command.Experience;
            entity.DateTimeUpdated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Employee updated successfully", true);
        }
    }
}
