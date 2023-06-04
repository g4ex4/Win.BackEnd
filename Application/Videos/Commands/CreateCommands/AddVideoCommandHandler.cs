using Application.Interfaces;
using Application.Subs.Commands.DeleteCommands;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Videos.Commands.CreateCommands
{
    public class AddVideoCommandHandler : IRequestHandler<AddVideoCommand, Response>
    {
        private readonly IVideoDbContext _videodbContext;
        private readonly ILogger<AddVideoCommandHandler> _logger;

        public AddVideoCommandHandler(IVideoDbContext videoDbContext,
            ILogger<AddVideoCommandHandler> logger)
        {
            _videodbContext = videoDbContext;
            _logger = logger;
        }

        public async Task<Response> Handle(AddVideoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                byte[] videoBytes;
                using (var memoryStream = new MemoryStream())
                {
                    await command.VideoFile.CopyToAsync(memoryStream);
                    videoBytes = memoryStream.ToArray();
                }
                
                var video = new Video
                {
                    VideoName = command.VideoFile.FileName,
                    Url = null, 
                    Media = videoBytes,
                    CourseId = command.CourseId
                };

                _videodbContext.Videos.Add(video);
                await _videodbContext.SaveChangesAsync(cancellationToken);

                return new Response(200, "Video added successfully", true);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new Response(400, $"An error occurred while adding the video: {e.Message}", false);
            }
        }
    }
}
