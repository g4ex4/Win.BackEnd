using Application.Interfaces;
using Application.Subs.Commands.DeleteCommands;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace Application.Videos.Commands.CreateCommands
{
    public class AddVideoCommandHandler : IRequestHandler<AddVideoCommand, VideoResponse>
    {
        private readonly IVideoDbContext _videodbContext;
        private readonly ILogger<AddVideoCommandHandler> _logger;

        public AddVideoCommandHandler(IVideoDbContext videoDbContext,
            ILogger<AddVideoCommandHandler> logger)
        {
            _videodbContext = videoDbContext;
            _logger = logger;
        }

        public async Task<VideoResponse> Handle(AddVideoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var videoFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "VideoFiles");
                if (!Directory.Exists(videoFolderPath))
                {
                    Directory.CreateDirectory(videoFolderPath);
                }
                //var uniqueFileName = Guid.NewGuid().ToString() + command.VideoFile.FileName;
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(command.VideoFile.FileName);

                // Полный путь к файлу
                var filePath = Path.Combine(videoFolderPath, uniqueFileName);

                // Сохраните файл на сервере
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await command.VideoFile.CopyToAsync(stream);

                }

                var video = new Video
                {
                    VideoName = command.VideoFile.FileName,
                    Url = videoFolderPath + "\\" + command.VideoFile.FileName,
                    DateTimeAdded = DateTime.UtcNow,
                    DateTimeUpdated = DateTime.UtcNow,

                    CourseId = command.CourseId
                };

                _videodbContext.Videos.Add(video);
                await _videodbContext.SaveChangesAsync(cancellationToken);

                return new VideoResponse(200, "Video added successfully", true, video);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new VideoResponse(400, $"An error occurred while adding the video: {e.Message}", false, null);
            }
        }
    }
}
