using Application.Empl.Commands.CreateCommands;
using Application.Empl.Commands.DeleteCommands;
using Application.Empl.Commands.UpdateCommands;
using Application.Empl.Queries;
using Application.Students.Commands.CreateCommands;
using Application.Students.Commands.UpdateCommands;
using Application.Students.Queries;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentAccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentAccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<StudentResponse> Register(CreateStudentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new StudentResponse(400, "Invalid input data", false, null, null);
            }
            var response = await _mediator.Send(request);

            return (StudentResponse)response;
        }

        [HttpPost("authorize")]
        public async Task<StudentResponse> Authorize(AuthorizeStudentCommand request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return new StudentResponse(400, "Email is required.", false, null, null);
            }

            if (string.IsNullOrEmpty(request.PasswordHash))
            {
                return new StudentResponse(400, "Password is required.", false, null, null);
            }

            var response = await _mediator.Send(request);
            return (StudentResponse)response;
        }

        [HttpPut("changePassword")]
        [Authorize]
        public async Task<PersonResponse> ChangePassword(ChangePasswordStudentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new PersonResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("resetPassword")]
        public async Task<PersonResponse> ResetPassword(StudentResetPasswordCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new PersonResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, [FromServices] IMediator mediator)
        {
            var command = new StudentConfirmEmailCommand { Email = email };
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

    }
}
