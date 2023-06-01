using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Empl.Queries
{
    public class GetMentorByIdQueryHandler : IRequestHandler<GetMentorByIdQuery, MentorLookupDto>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeDbContext _dbContext;

        public GetMentorByIdQueryHandler(IMapper mapper, IEmployeeDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<MentorLookupDto> Handle(GetMentorByIdQuery request, CancellationToken cancellationToken)
        {
            var mentor = await _dbContext.Employees
                .Where(e => e.Id == request.MentorId && e.RoleId == 2)
                .Select(e => new MentorLookupDto
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    DateTimeAdded = e.DateTimeAdded,
                    DateTimeUpdated = e.DateTimeUpdated,
                    JobTitle = e.JobTitle,
                    Experience = e.Experience,
                    Education = e.Education,
                    IsConfirmed = e.IsConfirmed
                })
                .FirstOrDefaultAsync(cancellationToken);

            return mentor;
        }
    }
}
