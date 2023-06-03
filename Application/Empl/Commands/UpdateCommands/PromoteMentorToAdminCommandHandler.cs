using Application.Interfaces;
using Domain.Common;
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
    public class PromoteMentorToAdminCommandHandler : IRequestHandler<PromoteMentorToAdminCommand, PersonResponse>
    {
        private readonly IEmployeeDbContext _dbContext;

        public PromoteMentorToAdminCommandHandler(IEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonResponse> Handle(PromoteMentorToAdminCommand request, CancellationToken cancellationToken)
        {
            var mentor = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == request.MentorId);
            if (mentor == null)
            {
                return new PersonResponse(404, "Mentor not found", false, null);
            }

            mentor.RoleId = 1;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var person = new Person
            {
                Id = mentor.Id,
                UserName = mentor.UserName
            };

            return new PersonResponse(200, "Mentor promoted to admin successfully", true, person);
        }
    }
}
