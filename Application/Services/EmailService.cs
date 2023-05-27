using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmailService
    {
        //public async Task SendEmailAsync(EmailRequest emailRequest, SMTPConfig config)
        //{
        //    MailMessage message = new MailMessage(config.SenderEmail,
        //        emailRequest.RecipientEmail, emailRequest.Body, emailRequest.Subject);
        //    message.IsBodyHtml = config.IsBodyHtml;

        //    SmtpClient smtpClient = new SmtpClient(config.ServerAddress, config.Port);
        //    smtpClient.EnableSsl = config.EnableSsl;
        //    smtpClient.UseDefaultCredentials = config.UseDefaultCredentials;
        //    smtpClient.Credentials = new NetworkCredential(config.SenderEmail, config.SenderPassword);

        //    smtpClient.Send(message);
        //}

        public async Task SendEmailAsync(string emailRequest)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress("1goldyshsergei1@gmail.com");
            emailMessage.To.Add(emailRequest);
            emailMessage.Subject = "Confirmation of registration";
            emailMessage.Body = $"Follow the link to confirm your registration: <a href='https://localhost:7090/Account/confirm-email?email={emailRequest}'>Follow this link</a>";
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
            emailMessage.Subject = "Confirmation of registration";
            emailMessage.Body = $"Follow the link to confirm your registration: <a href='https://localhost:7090/StudentAccount/confirm-email?email={emailRequest}'>Follow this link</a>";
            emailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("1goldyshsergei1@gmail.com", "woyjfdbxdoxgexjj");
            await smtpClient.SendMailAsync(emailMessage);
            smtpClient.Dispose();
        }

        public async Task SendEmailInfoAsync(string emailRequest)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress("1goldyshsergei1@gmail.com");
            emailMessage.To.Add(emailRequest);
            emailMessage.Subject = "Change Password";
            emailMessage.Body = "Password has been successfully changed";
            emailMessage.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential("1goldyshsergei1@gmail.com", "woyjfdbxdoxgexjj");
            await smtpClient.SendMailAsync(emailMessage);
            smtpClient.Dispose();
        }

        public async Task SendEmailResetPasswordAsync(string emailRequest, string tempPassword)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress("1goldyshsergei1@gmail.com");
            emailMessage.To.Add(emailRequest);
            emailMessage.Subject = "Log in using the temporary password and" +
                " change it to your new password";
            emailMessage.Body = $"Here is your temporary password:  {tempPassword}";
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
