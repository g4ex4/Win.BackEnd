using Application.Common.Abstracts;
using Application.Common.Config;
using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, Response>
    {
        private readonly UserManager<User> _userManager;
        private readonly IMemoryCache _memoryCache;
        private readonly EmailService _emailService;
        private readonly SMTPConfig _config;


        public RegisterUserHandler(
            UserManager<User> userManager,
            EmailService emailService,
            SMTPConfig config,
            IMemoryCache memoryCache)
        {
            _userManager = userManager;
            _memoryCache = memoryCache;
            _config = config;
            _emailService = emailService;
        }
        public async Task<Response> Handle(RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            if (!await SendEmailAsync(request.Email))
            {
                return new Response(400, "We've already sent the code to your email. Please chek it", false);
            }

            var result = await RegisterUser(request);

            if (!result.Succeeded)
            {
                string aggregatedErrorMessages = string.Join("\n", result.Errors
                    .Select(e => e.Description));

                throw new DException(aggregatedErrorMessages);
            }

            return new Response(200, "Initial user created", true);
        }
        private void SetUserProperties(User user, string fullName, string email)
        {
            user.FullName = fullName;
            user.Email = email;
            user.UserName = email;
        }

        private async Task<bool> SendEmailAsync(string email)
        {
            if (_memoryCache.TryGetValue(email, out _))
            {
                return false;
            }

            int code = CodeHelper.GetRandomCode(7);
            var emailRequest = new EmailRequest()
            {
                Body = $"{code}",
                Subject = $"Ваш код верификации",
                RecipientEmail = $"{email}",
            };
            await _emailService.SendEmailAsync(emailRequest, _config);
            _memoryCache.Set(email, code, DateTimeOffset.Now.AddMinutes(15));

            return true;
        }

        private async Task<IdentityResult> RegisterUser(RegisterUserRequest request)
        {
            var user = new User();
            SetUserProperties(user, request.FullName, request.Email);

            var result = await _userManager.CreateAsync(user, request.Password);

            return result;
        }
    }
}
