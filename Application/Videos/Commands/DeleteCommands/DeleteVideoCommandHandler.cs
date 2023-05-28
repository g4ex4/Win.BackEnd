using Application.Interfaces;
using Domain.Responses;
using MediatR;

namespace Application.Videos.Commands.DeleteCommands
{
    public class DeleteVideoCommandHandler : IRequestHandler<DeleteVideoCommand, Response>
    {
        private readonly IVideoDbContext _videoDbContext;

        public DeleteVideoCommandHandler(IVideoDbContext videoDbContext)
        {
            _videoDbContext = videoDbContext;
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
                return new Response(400, $"An error occurred while deleting the video: {e.Message}", false);
            }
        }
    }
}
