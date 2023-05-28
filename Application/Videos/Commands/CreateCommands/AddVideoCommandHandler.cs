using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using MediatR;

namespace Application.Videos.Commands.CreateCommands
{
    public class AddVideoCommandHandler : IRequestHandler<AddVideoCommand, Response>
    {
        private readonly IVideoDbContext _videodbContext;

        public AddVideoCommandHandler(IVideoDbContext videoDbContext)
        {
            _videodbContext = videoDbContext;
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
                return new Response(400, $"An error occurred while adding the video: {e.Message}", false);
            }
        }
    }
}
