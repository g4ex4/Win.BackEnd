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
    public class GetAllMentorsQueryHandler : IRequestHandler<GetAllMentorsQuery, List<MentorLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeDbContext _dbContext;

        public GetAllMentorsQueryHandler(IMapper mapper, IEmployeeDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<MentorLookupDto>> Handle(GetAllMentorsQuery request, CancellationToken cancellationToken)
        {
            var mentors = await _dbContext.Employees
                .Where(e => e.RoleId == 2)
                .Select(e => new MentorLookupDto
                {
                    Id = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    IsDeleted = e.IsDeleted,
                    DateTimeAdded = e.DateTimeAdded,
                    DateTimeUpdated = e.DateTimeUpdated,
                    JobTitle = e.JobTitle,
                    Experience = e.Experience,
                    Education = e.Education,
                    IsConfirmed = e.IsConfirmed
                })
                .ToListAsync(cancellationToken);

            return mentors;
        }
    }
}
