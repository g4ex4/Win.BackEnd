using Application.Repositories;
using Domain.Entities;
using Domain.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Users
{
    public class CheckCodeHandler : IRequestHandler<CheckCodeRequest, Response>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<User> _signInManager;

        public CheckCodeHandler(IMemoryCache memoryCache,
            IUserRepository userRepository,
            SignInManager<User> signInManager)
        {
            _memoryCache = memoryCache;
            _userRepository = userRepository;
            _signInManager = signInManager;
        }

        public async Task<Response> Handle(CheckCodeRequest request, CancellationToken cancellationToken)
        {
            if (_memoryCache.TryGetValue(request.Email, out _))
            {
                if (!await _userRepository.UpdateUserEmailConfirmedFlag(request.Email))
                    return new Response(404, $"User with {request.Email} email was't found", false);

                var user = await _userRepository.GetUserByEmailAsync(request.Email);
                await _signInManager.SignInAsync(user, isPersistent: false);

                return new Response(200, "Code confirmed successfully", true);
            }
            else
            {
                return new Response(400, "Time of code validity expired, please reregister to get a new code", false);
            }
        }
    }
}
