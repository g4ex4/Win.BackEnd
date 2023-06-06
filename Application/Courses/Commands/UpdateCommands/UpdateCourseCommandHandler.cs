using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Common.Exceptions;

namespace Application.Courses.Commands.UpdateCommands
{
    public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseResponse>
    {
        private readonly ICourseDbContext _dbContext;

        public UpdateCourseCommandHandler(ICourseDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CourseResponse> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Courses.FirstOrDefaultAsync(c => c.Id == command.CourseId, cancellationToken);
            if (entity == null || entity.MentorId != command.MentorId)
            {
                return new CourseResponse(null, 400, "Course or Mentor is not found", true);
            }

            entity.Title = command.Title;
            entity.Description = command.Description;
            entity.DateTimeUpdated = DateTime.UtcNow;

            if (command.ImageFile != null)
            {
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

                entity.ImageCourseName = uniqueFileName;
                entity.ImageCourseUrl = Path.Combine(imageFolderPath, uniqueFileName);
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new CourseResponse(entity, 200, "Course updated successfully", true);
        }
    }
}
