using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Commands.CreateCommands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, Response>
    {
        private readonly ICourseDbContext _CoursedbContext;
        private readonly IEmployeeDbContext _EmployeedbContext;

        public CreateCourseCommandHandler(ICourseDbContext courseDbContext,
            IEmployeeDbContext employeeDbContext)
        {
            _CoursedbContext = courseDbContext;
            _EmployeedbContext = employeeDbContext;
        }

        public async Task<Response> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            var mentor = await _EmployeedbContext.Employees
                .FirstOrDefaultAsync(e => e.Id == command.MentorId);

            if (mentor == null)
            {
                return new Response(404, "Mentor not found", false);
            }

            if (!mentor.IsConfirmed)
            {
                return new Response(403, "Access denied. Mentor is not confirmed.", false);
            }

            Course course = new Course
            {
                Title = command.Title,
                Description = command.Description,
                MentorId = command.MentorId,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };

            await _CoursedbContext.Courses.AddAsync(course);
            await _CoursedbContext.SaveChangesAsync(cancellationToken);

            return new CourseResponse(course, 200, "Course added successfully", true);
        }
    }
}
