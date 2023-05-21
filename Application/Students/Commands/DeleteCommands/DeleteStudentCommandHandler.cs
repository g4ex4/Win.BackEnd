using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteStudentCommandHandler :
        IRequestHandler<DeleteStudentCommand, Response>
    {
        private readonly IStudentDbContext _dbContext;

        public DeleteStudentCommandHandler(IStudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId);
            if (entity == null)
            {
                return new Response(400, "Student not found", false);
                //throw new NotFoundException(nameof(Employee), request.EmployeeId);
            }

            _dbContext.Students.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Student deleted successfully", true);
        }
    }
}
