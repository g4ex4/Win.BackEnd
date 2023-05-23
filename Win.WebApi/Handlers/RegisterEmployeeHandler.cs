using Application.Common.Exceptions;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Win.WebApi.Requests;

namespace Win.WebApi.Handlers
{
    //public class RegisterEmployeeHandler : IRequestHandler<RegisterEmployeeRequest, Response>
    //{
    //    private readonly UserManager<Employee> _userManager;
    //    private readonly EmailManager _emailManager;

    //    public RegisterEmployeeHandler(
    //        UserManager<Employee> userManager,
    //        EmailManager emailManager)
    //    {
    //        _userManager = userManager;
    //        _emailManager = emailManager;
    //    }
    //    public async Task<Response> Handle(RegisterEmployeeRequest request,
    //        CancellationToken cancellationToken)
    //    {
    //        if (!await _emailManager.SendEmailAsync(request.Email))
    //        {
    //            return new Response(400, "We've already sent the code to your email. Please chek it", false);
    //        }

    //        var result = await RegisterUser(request);

    //        if (!result.Succeeded)
    //        {
    //            string aggregatedErrorMessages = string.Join("\n", result.Errors
    //                .Select(e => e.Description));

    //            throw new DException(aggregatedErrorMessages);
    //        }
    //        return new Response(200, "Initial user created", true);
    //    }

    //    private void SetUserProperties(Employee user, string UserName, string email)
    //    {
    //        user.UserName = UserName;
    //        user.Email = email;
    //        user.UserName = email;
    //    }

    //    private async Task<IdentityResult> RegisterUser(RegisterEmployeeRequest request)
    //    {
    //        var Employee = new Employee();
    //        SetUserProperties(Employee, request.UserName, request.Email);

    //        var result = await _userManager.CreateAsync(Employee, request.PasswordHash);
    //        return result;
    //    }
    //}
}
