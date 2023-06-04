using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Empl.Commands.CreateCommands
{
    public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, Response>
    {
        private readonly IEmployeeDbContext _context;
        private readonly ILogger<ConfirmEmailCommandHandler> _logger;

        public ConfirmEmailCommandHandler(IEmployeeDbContext context,
             ILogger<ConfirmEmailCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Response> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _context.Employees.FirstOrDefault(x => x.Email == request.Email);
                if (user == null)
                    return new Response(404, "User is not found", false);

                user.EmailConfirmed = true;
                await _context.SaveChangesAsync(cancellationToken);

                return new Response(200, "You have verified your account", true);
            }
            catch (Exception ex)
            {
                _logger.LogError($" {ex}");
                return new Response(400, "Something went wrong " + ex.Message, false);
            }
        }
    }
}
