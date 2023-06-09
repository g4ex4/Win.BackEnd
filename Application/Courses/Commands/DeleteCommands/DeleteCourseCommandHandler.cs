using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Application.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Commands.DeleteCommands
{
    public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, Response>
    {
        private readonly ICourseDbContext _dbContext;
        private readonly IEmployeeDbContext _employeeDbContext;
        private readonly IVideoDbContext _videoDbContext;
        private readonly ISubDbContext _subsDbContext;

        public DeleteCourseCommandHandler(ICourseDbContext dbContext,
            IEmployeeDbContext employeeDbContext,
            IVideoDbContext videoDbContext,
            ISubDbContext subsDbContext)
        {
            _dbContext = dbContext;
            _employeeDbContext = employeeDbContext;
            _videoDbContext = videoDbContext;
            _subsDbContext = subsDbContext;
        }

        public async Task<Response> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Courses.FindAsync(new object[] { request.CourseId }, cancellationToken);

            if (entity == null)
            {
                return new Response(403, "Course is not found.", false);
            }

            var mentor = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == entity.UserId);

            if (mentor == null)
            {
                return new Response(403, "Mentor is not found.", false);
            }
            var video = await _videoDbContext.Videos.FirstOrDefaultAsync(v=>v.CourseId== request.CourseId);
            if (video != null)
            {
                _videoDbContext.Videos.Remove(video);
                await _videoDbContext.SaveChangesAsync(cancellationToken);
            }

            var subs = await _subsDbContext.Subs.FirstOrDefaultAsync(v => v.CourseId == request.CourseId);
            if (subs != null)
            {
                _subsDbContext.Subs.Remove(subs);
                await _subsDbContext.SaveChangesAsync(cancellationToken);
            }

            try
            {
                _dbContext.Courses.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                return new Response(403, ex.Message, true);
            }

            return new Response(200, "Course deleted successfully", true);
        }
    }
}
