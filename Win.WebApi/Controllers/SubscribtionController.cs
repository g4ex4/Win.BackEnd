using Application.Subs.Commands.CreateCommands;
using Application.Subs.Commands.DeleteCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscribtionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscribtionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("addSubscribtion")]
        public async Task<Response> AddSubscribtion(SubscribeToCourseCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new Response(400, "Invalid input data", false);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpDelete("unsubscribe")]
        public async Task<Response> UnsubscribeFromCourse([FromBody] UnsubscribeFromCourseCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }
    }
}
