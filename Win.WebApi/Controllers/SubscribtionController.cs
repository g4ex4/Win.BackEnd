using Application.Empl.Commands.CreateCommands;
using Application.Subs.Commands.CreateCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubscribtionController : ControllerBase
    {
        private readonly SqlServerContext _context;
        private readonly IMediator _mediator;


        public SubscribtionController(SqlServerContext context, IMediator mediator)
        {
            _context = context;
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
            //_emailService.SendEmailAsync(request.Email);

            return response;
        }
    }
}
