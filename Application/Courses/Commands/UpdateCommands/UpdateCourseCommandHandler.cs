using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.Courses.Commands.UpdateCommands
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, Response>
    {
        private readonly ICourseDbContext _dbContext;

        public UpdateCourseCommandHandler(ICourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == command.CourseId, cancellationToken);
            if (entity == null || entity.MentorId != command.MentorId)
            {
                return new Response(400, "Course or Mentor is not found", true);
            }

            entity.Title = command.Title;
            entity.Description = command.Description;
            entity.DateTimeUpdated = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Course updated successfully", true);
        }
    }
}
