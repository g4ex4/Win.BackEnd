using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommandHandler :
        IRequestHandler<DeleteEmplCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;

        public DeleteEmplCommandHandler(IEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(DeleteEmplCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
            if (entity == null)
            {
                return new Response(400, "Employee not found", false);
                //throw new NotFoundException(nameof(Employee), request.EmployeeId);
            }

            _dbContext.Employees.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Employee deleted successfully", true);
        }
    }
}
