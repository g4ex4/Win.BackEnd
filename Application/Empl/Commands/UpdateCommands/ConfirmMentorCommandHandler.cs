using Application.Interfaces;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Commands.UpdateCommands
{
    public class ConfirmMentorCommandHandler : IRequestHandler<ConfirmMentorCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;

        public ConfirmMentorCommandHandler(IEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Response> Handle(ConfirmMentorCommand request, CancellationToken cancellationToken)
        {
            var mentor = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == request.MentorId);
            if (mentor == null)
            {
                return new Response(404, "Mentor not found", false);
            }

            mentor.IsConfirmed = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            return new Response(200, "Mentor confirmed successfully", true);
        }
    }
}
