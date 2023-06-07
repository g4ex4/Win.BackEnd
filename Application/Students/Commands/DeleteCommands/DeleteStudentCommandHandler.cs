using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteStudentCommandHandler :
        IRequestHandler<DeleteStudentCommand, Response>
    {
        private readonly IStudentDbContext _dbContext;
        private readonly ISubDbContext _subsDbContext;

        public DeleteStudentCommandHandler(IStudentDbContext dbContext, ISubDbContext subsDbContext)
        {
            _dbContext = dbContext;
            _subsDbContext = subsDbContext;
        }

        public async Task<Response> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Students.FirstOrDefaultAsync(x => x.Id == request.StudentId);
            if (entity == null)
            {
                return new Response(400, "Student not found", false);
            }

            var subs = await _subsDbContext.Subs.FirstOrDefaultAsync(v => v.StudentId == request.StudentId);
            if (subs != null)
            {
                _subsDbContext.Subs.Remove(subs);
                await _subsDbContext.SaveChangesAsync(cancellationToken);
            }

            _dbContext.Students.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Student deleted successfully", true);
        }
    }
}
