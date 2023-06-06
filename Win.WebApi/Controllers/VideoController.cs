using Application.Videos.Commands.CreateCommands;
using Application.Videos.Commands.DeleteCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        public async Task<VideoResponse> AddVideo(int courseId, IFormFile videoFile)
        {
            var command = new AddVideoCommand
            {
                CourseId = courseId,
                VideoFile = videoFile,
            };

            var validator = new AddVideoCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return new VideoResponse(400, "Invalid input data", false, null);
            }

            var response = await _mediator.Send(command);

            return response;
        }

        [HttpDelete("{videoId}")]
        [Authorize(Roles = "1,2")]
        public async Task<Response> DeleteVideo(
            [Range(1, int.MaxValue, ErrorMessage = "Invalid course ID.")] int videoId)
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
