using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class SendEmailCommand : IRequest
    {
        public EmailRequest EmailRequest { get; }

        public SendEmailCommand(EmailRequest emailRequest)
        {
            EmailRequest = emailRequest;
        }
    }
}
