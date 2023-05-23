using Application.Common.Abstracts;
using Application.Common.Exceptions;
using Application.Services;
using Domain.Entities;
using Domain.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Managers
{
    public class EmailManager
    {
        //private readonly IMemoryCache _memoryCache;
        //private readonly EmailService _emailService;
        //private readonly SMTPConfig _config;
        //private readonly UserManager<Employee> _userManager;
        //public EmailManager(IMemoryCache memoryCache,
        //    EmailService emailService,
        //    Config config,
        //    UserManager<Employee> userManager)
        //{
        //    _memoryCache = memoryCache;
        //    _emailService = emailService;
        //    _config = config.SMTPConfig;
        //    _userManager = userManager;
        //}

        //public async Task<Response> GetCurrentUserEmailAsync(ClaimsPrincipal currentUserClaims)
        //{
        //    var user = await _userManager.GetUserAsync(currentUserClaims);

        //    if (user == null)
        //    {
        //        return new Response(400, "Current user not found", false);
        //        throw new DException("User not found");
        //    }

        //    return new Response(200, "User recieved", true);
        //}

        //public async Task<bool> SendEmailAsync(string email)
        //{
        //    if (_memoryCache.TryGetValue(email, out _))
        //        return false;

        //    int code = CodeHelper.GetRandomCode(7);
        //    var emailRequest = new EmailRequest()
        //    {
        //        Body = $"{code}",
        //        Subject = $"Ваш код верификации",
        //        RecipientEmail = $"{email}",
        //    };

        //    await _emailService.SendEmailAsync(emailRequest, _config);
        //    _memoryCache.Set(email, code, DateTimeOffset.Now.AddMinutes(15));

        //    return true;
        //}

        //public async Task<Response> ChangeEmail(string email)
        //{
        //    var user = await _userManager.FindByEmailAsync(email);

        //    if (user == null)
        //    {
        //        return new Response(400, "User not found", false);
        //        throw new DException("User not found");
        //    }

        //    await SendEmailAsync(email);
        //    return new Response(200, "User email will change", true);
        //}
    }
}
