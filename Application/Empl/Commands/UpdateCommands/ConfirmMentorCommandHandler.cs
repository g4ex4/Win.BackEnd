﻿using Application.Interfaces;
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
    public class ConfirmMentorCommandHandler : IRequestHandler<ConfirmMentorCommand, PersonResponse>
    {
        private readonly IEmployeeDbContext _dbContext;

        public ConfirmMentorCommandHandler(IEmployeeDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PersonResponse> Handle(ConfirmMentorCommand request, CancellationToken cancellationToken)
        {
            var mentor = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == request.MentorId);
            if (mentor == null)
            {
                return new PersonResponse(404, "Mentor not found", false, null);
            }

            mentor.IsConfirmed = true;
            await _dbContext.SaveChangesAsync(cancellationToken);

            var person = new Person
            {
                Id = mentor.Id,
                UserName = mentor.UserName
            };

            return new PersonResponse(200, "Mentor confirmed successfully", true, person);
        }
    }
}
