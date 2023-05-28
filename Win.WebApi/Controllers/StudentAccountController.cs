using Application.Empl.Commands.CreateCommands;
using Application.Empl.Commands.DeleteCommands;
using Application.Empl.Commands.UpdateCommands;
using Application.Students.Commands.CreateCommands;
using Application.Students.Commands.UpdateCommands;
using Domain.Responses;
using MediatR;
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
        public async Task<Response> Register(CreateStudentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new StudentResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("authorize")]
        public async Task<Response> Authorize(AuthorizeStudentCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpPut("changePassword")]
        public async Task<Response> ChangePassword(ChangePasswordStudentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new StudentResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("resetPassword")]
        public async Task<Response> ResetPassword(StudentResetPasswordCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new StudentResponse(400, "Invalid input data", false, null);
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

        [HttpDelete("Delete-User")]
        public async Task<Response> DeleteStudent(DeleteStudentCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }
    }
}
