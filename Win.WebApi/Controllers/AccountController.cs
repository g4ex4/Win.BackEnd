using Application.Interfaces;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Persistance;
using System.Net.Mail;
using Win.WebApi.DtoModel;
using System.Net;
using System.Net.Security;
using Application.Employees.Requests;
using Win.WebApi.Requests;
using MediatR;

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

        [HttpPost]
        [Route("create")]
        public async Task<EmployeeResponse> CreateDepartment([FromBody] EmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return new EmployeeResponse(400, "Invalid input data", false, null);
            }
            if (!employeeDto.Email.Contains("@"))
            {
                return new EmployeeResponse(400, "Email is not correct", false, null);
            }
            //if (_context.Employees.Count(x => x.Email.Trim() == employeeDto.Email.Trim()) > 0)
            //{
            //    return new EmployeeResponse(400, "Error, Email already exists", false, null);  //Ошибка, электронная почта уже существует
            //}
            Employee employee = null;
            try
            {
                employee = new Employee
                {
                    UserName = employeeDto.UserName,
                    Email = employeeDto.Email,
                    PasswordHash = employeeDto.PasswordHash,
                    JobTitle = employeeDto.JobTitle,
                    Experience = employeeDto.Experience,
                    Education = employeeDto.Education,
                    DateTimeAdded = DateTime.UtcNow,
                    DateTimeUpdated = DateTime.UtcNow,
                    
                };
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();

                var emailMessage = new MailMessage();
                emailMessage.From = new MailAddress("1goldyshsergei1@gmail.com");
                emailMessage.To.Add(employeeDto.Email);
                emailMessage.Subject = "Подтверждение регистрации";
                emailMessage.Body = $"Пройдите по ссылке для подтверждения регистрации: <a href='https://localhost:7090/Account/confirm-email?email={employeeDto.Email}'>Перейдите по ссылке</a>";
                emailMessage.IsBodyHtml = true;

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.UseDefaultCredentials = false;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("1goldyshsergei1@gmail.com", "woyjfdbxdoxgexjj");
                await smtpClient.SendMailAsync(emailMessage);
                smtpClient.Dispose();

            }
            catch (Exception ex)
            {
                var response = new EmployeeResponse(400, "Error occurred while processing the request", false, employee);
                return response;
            }
            return new EmployeeResponse(200, "A confirmation link has been sent to your email", false, employee); //Ссылка для подтверждения отправлена на вашу почту


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

        [HttpDelete("Delete-User")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = _context.Employees.FirstOrDefault(x => x.Id == id);
                if (user == null)
                    return Content("пользователь не найден");
                
                _context.Employees.Remove(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("что-то пошло не так " + ex.Message);
            }
            return Ok("Пользователь удалён");

        }

        [HttpPost("register")]
        public async Task<Response> Register(RegisterEmployeeRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

    }
}
