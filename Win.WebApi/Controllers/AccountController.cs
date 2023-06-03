using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Empl.Commands.CreateCommands;
using Application.Empl.Commands.DeleteCommands;
using Application.Empl.Commands.UpdateCommands;
using Microsoft.AspNetCore.Authorization;
using Application.Empl.Queries;
using Application.Interfaces;
using Persistance;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator, BaseDbContext employeeDbContext)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<PersonResponse> Register(RegisterEmployeeCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new PersonResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPut("changePassword")]
        [Authorize(Roles = "2")]
        public async Task<Response> ChangePassword(ChangePasswordEmployeeCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new PersonResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return (PersonResponse)response;
        }

        [HttpPost("authorize")]
        public async Task<PersonResponse> Authorize(AuthorizeEmployeeCommand request)
        {
            var response = await _mediator.Send(request);
            return (PersonResponse)response;
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, [FromServices] IMediator mediator)
        {
            var command = new ConfirmEmailCommand { Email = email };
            var response = await mediator.Send(command);

            if (response.IsSuccess)
            {
                return Ok(response.Message);
            }
            else
            {
                return BadRequest(response.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = "2")]
        public async Task<Response> Update(UpdateEmoloyeeCommand request)
        {
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("resetPassword")]
        public async Task<Response> ResetPassword(ResetPasswordCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new PersonResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }


    }
}
