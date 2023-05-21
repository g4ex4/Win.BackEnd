using System.Net.Mail;
using System.Net;
using Win.WebApi.Requests;
using Win.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace Win.WebApi.Services
{
    public class EmailService
    {
        public async Task SendEmailAsync(EmailRequest emailRequest, SMTPConfig config)
        {
            MailMessage message = new MailMessage(config.SenderEmail,
                emailRequest.RecipientEmail, emailRequest.Body, emailRequest.Subject);
            message.IsBodyHtml = config.IsBodyHtml;

            SmtpClient smtpClient = new SmtpClient(config.ServerAddress, config.Port);
            smtpClient.EnableSsl = config.EnableSsl;
            smtpClient.UseDefaultCredentials = config.UseDefaultCredentials;
            smtpClient.Credentials = new NetworkCredential(config.SenderEmail, config.SenderPassword);

            smtpClient.Send(message);
        }

        public async Task SendEmailAsync(string emailRequest)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress("1goldyshsergei1@gmail.com");
            emailMessage.To.Add(emailRequest);
            emailMessage.Subject = "Подтверждение регистрации";
            emailMessage.Body = $"Пройдите по ссылке для подтверждения регистрации: <a href='https://localhost:7090/Account/confirm-email?email={emailRequest}'>Перейдите по ссылке</a>";
            emailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("1goldyshsergei1@gmail.com", "woyjfdbxdoxgexjj");
            await smtpClient.SendMailAsync(emailMessage);
            smtpClient.Dispose();
        }

        public async Task SendStudentEmailAsync(string emailRequest)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress("1goldyshsergei1@gmail.com");
            emailMessage.To.Add(emailRequest);
            emailMessage.Subject = "Подтверждение регистрации";
            emailMessage.Body = $"Пройдите по ссылке для подтверждения регистрации: <a href='https://localhost:7090/StudentAccount/confirm-email?email={emailRequest}'>Перейдите по ссылке</a>";
            emailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("1goldyshsergei1@gmail.com", "woyjfdbxdoxgexjj");
            await smtpClient.SendMailAsync(emailMessage);
            smtpClient.Dispose();
        }


    }
}
