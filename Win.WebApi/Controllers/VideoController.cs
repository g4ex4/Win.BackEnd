using Application.Videos.Commands.CreateCommands;
using Application.Videos.Commands.DeleteCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VideoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public VideoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Authorize(Roles = "2")]
        public async Task<Response> AddVideo(int courseId, IFormFile videoFile)
        {
            var command = new AddVideoCommand
            {
                CourseId = courseId,
                VideoFile = videoFile
            };

            var response = await _mediator.Send(command);

            return new Response(response.StatusCode, response.Message, response.IsSuccess);
        }

        [HttpDelete("{videoId}")]
        [Authorize(Roles = "2")]
        public async Task<Response> DeleteVideo(int videoId)
        {
            var command = new DeleteVideoCommand
            {
                VideoId = videoId
            };

            var response = await _mediator.Send(command);

            return new Response(response.StatusCode, response.Message, response.IsSuccess);
        }

    }
}
