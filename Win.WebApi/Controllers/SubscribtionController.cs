using Application.Courses.Queries.GetCourseList;
using Application.Subs.Commands.CreateCommands;
using Application.Subs.Commands.DeleteCommands;
using Application.Subs.Queries;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<ActionResult<SubscriptionResponse>> AddSubscribtion(SubscribeToCourseCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new SubscriptionResponse(null, 400, "Invalid input data", false);
            }

            var response = await _mediator.Send(request);

            if (!response.IsSuccess)
            {
                return StatusCode(response.StatusCode, new { Message = response.Message });
            }

            return Ok(response);
        }

        [HttpDelete("unsubscribe")]
        [Authorize]
        public async Task<ActionResult<Response>> UnsubscribeFromCourse([FromBody] UnsubscribeFromCourseCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        [HttpGet("getAllSubscription")]
        public async Task<ActionResult<SubscriptionListVm>> GetAllSubscription()
        {
            var query = new GetAllSubscriptionQuery();
            var subsList = await _mediator.Send(query);

            var response = new Response(200, "Subscriptions retrieved successfully", true);
            return Ok(new { Response = response, Subs = subsList });
        }

        [HttpGet("getSubscriptionList/{studentId}")]
        public async Task<ActionResult<SubscriptionListVm>> GetSubscriptionList(int studentId)
        {
            var query = new GetSubscriptionListQuery { StudentId = studentId };
            var subscriptionList = await _mediator.Send(query);

            return Ok(new { Subscriptions = subscriptionList.Subs });
        }
    }
}
