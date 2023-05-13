using Application.Interfaces;
using MediatR;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Application.Courses.Queries.GetCourseList
{
    public class GetCourseListQueryHandler :
        IRequestHandler<GetCourseListQuery, CourseListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICourseDbContext _dbContext;

        public GetCourseListQueryHandler(IMapper mapper, ICourseDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<CourseListVm> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var coursesQuery = await _dbContext.Courses.Where(course => course.MentorId == request.MentorId)
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CourseListVm { Courses = coursesQuery};
        }
    }
}
