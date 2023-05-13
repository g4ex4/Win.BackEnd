using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Courses.Commands.CreateCommands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseResponse>
    {
        private readonly ICourseDbContext _dbContext;

        public CreateCourseCommandHandler(ICourseDbContext courseDbContext)
        {
            _dbContext = courseDbContext;
        }

        public async Task<CourseResponse> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            Course course = new Course
            {
                Title = command.Title,
                Description = command.Description,
                MentorId = command.MentorId,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
            };
            await _dbContext.Courses.AddAsync(course);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return new CourseResponse(course, 200, "Course added successfully", true);
        }
    }
}
