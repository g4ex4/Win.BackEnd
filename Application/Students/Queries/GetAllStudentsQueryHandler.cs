using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries
{
    public class GetAllStudentsQueryHandler : IRequestHandler<GetAllStudentsQuery, List<StudentLookupDto>>
    {
        private readonly IMapper _mapper;
        private readonly IStudentDbContext _dbContext;

        public GetAllStudentsQueryHandler(IMapper mapper, IStudentDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<List<StudentLookupDto>> Handle(GetAllStudentsQuery request, CancellationToken cancellationToken)
        {
            var mentors = await _dbContext.Students
                .Select(e => new StudentLookupDto
                {

                    Id = e.Id,
                    UserName = e.UserName,
                    Email = e.Email,
                    DateTimeAdded = e.DateTimeAdded,
                    DateTimeUpdated = e.DateTimeUpdated,
                })
                .ToListAsync(cancellationToken);

            return mentors;
        }
    }
    
}
