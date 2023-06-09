using Application.Common.Config;
using Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class EmailHandler : IRequestHandler<SendEmailCommand, Unit>
    {
        private readonly EmailService _emailService;
        private readonly SMTPConfig _config;

        public EmailHandler(EmailService emailService, SMTPConfig config)
        {
            _emailService = emailService;
            _config = config;
        }

        public async Task<Unit> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {
            await _emailService.SendEmailAsync(request.EmailRequest, _config);

            return Unit.Value;
        }
    }
}
