using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Students.Commands.CreateCommands
{
    public class StudentConfirmEmailCommandHandler : IRequestHandler<StudentConfirmEmailCommand, Response>
    {
        private readonly IStudentDbContext _context;
        private readonly ILogger<StudentConfirmEmailCommandHandler> _logger;

        public StudentConfirmEmailCommandHandler(IStudentDbContext context,
            ILogger<StudentConfirmEmailCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Response> Handle(StudentConfirmEmailCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = _context.Students.FirstOrDefault(x => x.Email == request.Email);
                if (user == null)
                    return new Response(404, "User is not found", false);

                user.EmailConfirmed = true;
                await _context.SaveChangesAsync(cancellationToken);
                
                return new Response(200, "You have verified your account", true);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
                return new Response(400, "Something went wrong " + ex.Message, false);
            }
        }

    }
}
