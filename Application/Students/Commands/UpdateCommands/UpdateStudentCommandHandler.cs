using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.Empl.Commands.UpdateCommands
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Response>
    {
        private readonly IStudentDbContext _dbContext;

        public UpdateStudentCommandHandler(IStudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdateStudentCommand command, CancellationToken cancellationToken)
        {
            //var entity = await _dbContext.Employees.FirstOrDefaultAsync(c => c.Id == command.Id, cancellationToken);
            //if (entity == null)
            //{
            //    //throw new NotFoundException(nameof(Employee), command.Id);
            //}

            //entity.JobTitle = command.JobTitle;
            //entity.Experience = command.Experience;
            //entity.DateTimeUpdated = DateTime.UtcNow;

            //await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Employee updated successfully", true);
        }
    }
}
