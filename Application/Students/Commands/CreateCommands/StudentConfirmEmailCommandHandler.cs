using Application.Interfaces;
using Domain.Responses;
using MediatR;

namespace Application.Students.Commands.CreateCommands
{
    public class StudentConfirmEmailCommandHandler : IRequestHandler<StudentConfirmEmailCommand, Response>
    {
        private readonly IStudentDbContext _context;

        public StudentConfirmEmailCommandHandler(IStudentDbContext context)
        {
            _context = context;
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
                return new Response(400, "Something went wrong " + ex.Message, false);
            }
        }

    }
}
