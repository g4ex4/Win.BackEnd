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

        public DeleteCourseCommandHandler(ICourseDbContext dbContext, IEmployeeDbContext employeeDbContext)
        {
            _dbContext = dbContext;
            _employeeDbContext = employeeDbContext;
        }

        public async Task<Response> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Courses.FindAsync(new object[] { request.CourseId }, cancellationToken);

            if (entity == null)
            {
                return new Response(403, "Course is not found.", false);
            }

            var mentor = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == entity.MentorId);

            if (mentor == null)
            {
                return new Response(403, "Mentor is not found.", false);
            }

            _dbContext.Courses.Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Course deleted successfully", true);
        }
    }
}
