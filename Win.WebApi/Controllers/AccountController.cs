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
using Win.WebApi.Requests;
using MediatR;
using Application.Empl.Commands.CreateCommands;
using Win.WebApi.Services;
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
        private readonly EmailService _emailService;

        public AccountController(SqlServerContext context, IMediator mediator, EmailService emailService)
        {
            _context = context;
            _mediator = mediator;
            _emailService = emailService;
        }

        
            //Закоментировал проверка в dto model 
            //if (!employeeDto.Email.Contains("@"))
            //{
            //    return new EmployeeResponse(400, "Email is not correct", false, null);
            //}

            //if (_context.Employees.Count(x => x.Email.Trim() == employeeDto.Email.Trim()) > 0)
            //{
            //    return new EmployeeResponse(400, "Error, Email already exists", false, null);  //Ошибка, электронная почта уже существует
            //}
    

        [HttpPost("register")]
        public async Task<Response> Register(CreateEmployeeCommand request)
        {
            if (!ModelState.IsValid)
            {
                return new EmployeeResponse(400, "Invalid input data", false, null);
            }
            var response = await _mediator.Send(request);
            _emailService.SendEmailAsync(request.Email);

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


        //[HttpPut]
        //[Route("ChangePassword", Name = "ChangePassword")]
        //public async Task<IActionResult> ChangePassword(string email, string oldPassword, string newPassword, string repeatnewPassword)
        //{

        //    var user = _context.Employees.FirstOrDefault(x => x.Email == email);

        //    if (user != null)
        //    {
        //        if (user.PasswordHash == oldPassword)
        //        {
        //            if (oldPassword != user.PasswordHash)
        //                return BadRequest("старый пароль не верен");
        //            if (newPassword != repeatnewPassword)
        //                return BadRequest("Поля нового пароля не соответствуют друг другу");
        //            if (newPassword == user.PasswordHash)
        //                return BadRequest("Новый пароль не может быть такимже как старый");


        //            user.PasswordHash = newPassword;
        //            var response = await _mediator.Send(user);

        //            //await _context.SaveChangesAsync();


        //            var emailMessage = new MimeMessage();

        //            string message = $"Пройдите по ссылке для подтверждения изменения пароля: <a href=http://localhost:5143/api/Account/confirm-password?email=" + user.Email + ">Перейдите по ссылке</a>";
        //            emailMessage.From.Add(new MailboxAddress("", "sstatus200@mail.ru"));
        //            emailMessage.To.Add(new MailboxAddress("", user.Email));
        //            //emailMessage.Subject = subject;
        //            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //            {
        //                Text = message
        //            };
        //            using (var client = new SmtpClient())
        //            {
        //                await client.ConnectAsync("smtp.mail.ru", 465, true);
        //                await client.AuthenticateAsync("sstatus200@mail.ru", "qbfBDzqGgCvt6L3kRewK");
        //                await client.SendAsync(emailMessage);

        //                await client.DisconnectAsync(true);
        //            }

        //            return Ok("Вам на почту отправлено сообщение, пройдите по ссылке для подтверждения");
        //        }
        //        else if (user.TempPassword == oldPassword)
        //        {

        //            if (EncryptPassword(oldPassword) != user.TempPassword)
        //                return BadRequest("старый пароль не верен");
        //            if (newPassword != repeatnewPassword)
        //                return BadRequest("Поля нового пароля не соответствуют друг другу");
        //            if (EncryptPassword(newPassword) == user.Password)
        //                return BadRequest("Новый пароль не может быть такимже как старый");
        //            user.Password = EncryptPassword(newPassword);
        //            user.TempPassword = null;

        //            await _context.SaveChangesAsync();

        //            var emailMessage = new MimeMessage();
        //            string message = $"Вы успешно создали новый пароль";
        //            emailMessage.From.Add(new MailboxAddress("", "sstatus200@mail.ru"));
        //            emailMessage.To.Add(new MailboxAddress("", user.Email));
        //            //emailMessage.Subject = subject;
        //            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        //            {
        //                Text = message
        //            };
        //            using (var client = new SmtpClient())
        //            {
        //                await client.ConnectAsync("smtp.mail.ru", 465, true);
        //                await client.AuthenticateAsync("sstatus200@mail.ru", "qbfBDzqGgCvt6L3kRewK");
        //                await client.SendAsync(emailMessage);

        //                await client.DisconnectAsync(true);
        //            }
        //            return Ok("Вы успешно создали новый пароль");
        //        }
        //        else
        //        {
        //            return BadRequest("Вы ввели неверный пароль");
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Пользователь не найден");
        //    }
        //}


        [HttpDelete("Delete-User")]
        public async Task<Response> DeleteEmployee(DeleteEmplCommand request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

    }
}
