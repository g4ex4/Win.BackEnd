using Application.Interfaces;
using Application.Videos.Commands.CreateCommands;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Videos.Commands.DeleteCommands
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, Response>
    {
        private readonly IVideoDbContext _videoDbContext;
        private readonly ILogger<DeleteVideoCommandHandler> _logger;

        public DeleteVideoCommandHandler(IVideoDbContext videoDbContext,
            ILogger<DeleteVideoCommandHandler> logger)
        {
            _videoDbContext = videoDbContext;
            _logger = logger;
        }

        public async Task<Response> Handle(DeleteVideoCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var video = await _videoDbContext.Videos.FindAsync(command.VideoId);
                if (video == null)
                {
                    return new Response(404, "Video not found", false);
                }

                _videoDbContext.Videos.Remove(video);
                await _videoDbContext.SaveChangesAsync(cancellationToken);

                return new Response(200, "Video deleted successfully", true);
            }
            catch (Exception e)
            {
                _logger.LogError($"{e}");
                return new Response(400, $"An error occurred while deleting the video: {e.Message}", false);
            }
        }
    }
}
