using Application.Courses.Queries.GetCourseList;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Subs.Queries
{
    public class GetSubscriptionListQueryHandler : IRequestHandler<GetSubscriptionListQuery, SubscriptionListVm>
    {
        private readonly IMapper _mapper;
        private readonly ISubDbContext _dbContext;

        public GetSubscriptionListQueryHandler(IMapper mapper, ISubDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<SubscriptionListVm> Handle(GetSubscriptionListQuery request, CancellationToken cancellationToken)
        {
            var subsQuery = await _dbContext.Subs.Where(subs => subs.StudentId == request.StudentId)
                .ProjectTo<SubscriptionLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new SubscriptionListVm { Subs = subsQuery };
        }
    }
}
