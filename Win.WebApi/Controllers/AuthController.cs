using Application.Managers;
using Application.Users;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Win.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AuthManager _authManager;

        public AuthController(IMediator mediator, AuthManager authManager)
        {
            _mediator = mediator;
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<Response> Register(RegisterUserRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpPost("login")]
        public async Task<Response> Login(LoginUserRequest request)
        {
            var response = await _mediator.Send(request);
            return response;
        }

        [HttpGet("getCurrentUser")]
        public async Task<CurrentUserResponse> GetCurrentUser()
        {
            ClaimsPrincipal currentUserClaims = User;
            return await _authManager.GetCurrentUser(currentUserClaims);
        }
    }
}
