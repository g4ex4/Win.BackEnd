using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Application.Common.Exceptions;

namespace Application.Courses.Commands.DeleteCommands
{
    public class DeleteCourseCommandHandler :
        IRequestHandler<DeleteCourseCommand, Response>
    {
        private readonly ICourseDbContext _dbContext;

        public DeleteCourseCommandHandler(ICourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Courses.FindAsync(
                new object[] {request.CourseId}, cancellationToken);
            if (entity == null || entity.MentorId != request.MentorId)
            {
                throw new NotFoundException(nameof(entity), request.CourseId);
            }
            _dbContext.Courses.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Course deleted successfully", true);
        }
    }
}
