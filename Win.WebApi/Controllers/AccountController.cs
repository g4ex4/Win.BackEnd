using Domain.Responses;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using MediatR;
using Application.Empl.Commands.CreateCommands;
using Application.Empl.Commands.DeleteCommands;
using Application.Empl.Commands.UpdateCommands;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SqlServerContext _context;
        private readonly IMediator _mediator;
        

        public AccountController(SqlServerContext context, IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

    

        [HttpPost("register")]
        public async Task<Response> Register(RegisterEmployeeCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new EmployeeResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);
            //_emailService.SendEmailAsync(request.Email);

            return response;
        }


        [HttpPut("changePassword")]
        public async Task<Response> ChangePassword(ChangePasswordEmployeeCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new EmployeeResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);

            return response;
        }

        [HttpPost("authorize")]
        public async Task<Response> Authorize(AuthorizeEmployeeCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }



        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email)
        {
            try
            {
                var user = _context.Employees.FirstOrDefault(x => x.Email == email);
                if (user == null)
                    return Content("пользователь не найден");

                user.IsConfirmed = true;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("что-то пошло не так " + ex.Message);
            }
            return Ok("Вы подтвердили свой аккаунт");


        }

        [HttpPut("Update")]
        public async Task<Response> Update(UpdateEmoloyeeCommand request)
        {
            var response = await _mediator.Send(request);

            return response;
        }


        


        [HttpDelete("Delete-User")]
        public async Task<Response> DeleteEmployee(DeleteEmplCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

    }
}
