﻿using Application.Empl.Commands.CreateCommands;
using Application.Empl.Commands.DeleteCommands;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Persistance;
using Win.WebApi.Services;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentAccountController : ControllerBase
    {
        private readonly SqlServerContext _context;
        private readonly IMediator _mediator;
        private readonly EmailService _emailService;

        public StudentAccountController(SqlServerContext context, IMediator mediator, EmailService emailService)
        {
            _context = context;
            _mediator = mediator;
            _emailService = emailService;
        }

        [HttpPost("register")]
        public async Task<Response> Register(CreateStudentCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new EmployeeResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);
            _emailService.SendStudentEmailAsync(request.Email);

            return response;
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email)
        {
            try
            {
                var user = _context.Students.FirstOrDefault(x => x.Email == email);
                if (user == null)
                    return Content("пользователь не найден");
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("что-то пошло не так " + ex.Message);
            }
            return Ok("Вы подтвердили свой аккаунт");


        }

        [HttpDelete("Delete-User")]
        public async Task<Response> DeleteStudent(DeleteStudentCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }
    }
}