using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Courses.Commands.CreateCommands
{
    public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, CourseResponse>
    {
        private readonly ICourseDbContext _courseDbContext;
        private readonly IEmployeeDbContext _employeeDbContext;

        public CreateCourseCommandHandler(ICourseDbContext courseDbContext,
            IEmployeeDbContext employeeDbContext)
        {
            _courseDbContext = courseDbContext;
            _employeeDbContext = employeeDbContext;
        }

        public async Task<CourseResponse> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
        {
            var mentor = await _employeeDbContext.Employees.FirstOrDefaultAsync(e => e.Id == command.MentorId);

            if (mentor == null)
            {
                return new CourseResponse(null, 404, "Mentor not found", false);
            }

            if (!mentor.IsConfirmed)
            {
                return new CourseResponse(null, 403, "Access denied. Mentor is not confirmed.", false);
            }

            var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "ImageFiles");
            if (!Directory.Exists(imageFolderPath))
            {
                Directory.CreateDirectory(imageFolderPath);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(command.ImageFile.FileName);
            var filePath = Path.Combine(imageFolderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await command.ImageFile.CopyToAsync(stream);
            }

            Course course = new Course
            {
                Title = command.Title,
                Description = command.Description,
                MentorId = command.MentorId,
                DateTimeAdded = DateTime.UtcNow,
                DateTimeUpdated = DateTime.UtcNow,
                ImageCourseName = uniqueFileName,
                ImageCourseUrl = Path.Combine(imageFolderPath, uniqueFileName)
            };

            await _courseDbContext.Courses.AddAsync(course);
            await _courseDbContext.SaveChangesAsync(cancellationToken);

            return new CourseResponse(course, 200, "Course added successfully", true);
        }


    }
}
