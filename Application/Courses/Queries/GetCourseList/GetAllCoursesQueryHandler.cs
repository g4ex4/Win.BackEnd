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

namespace Application.Courses.Queries.GetCourseList
{
    public class GetAllCoursesQueryHandler : IRequestHandler<GetAllCoursesQuery, CourseListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICourseDbContext _dbContext;

        public GetAllCoursesQueryHandler(IMapper mapper, ICourseDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<CourseListVm> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            var coursesQuery = await _dbContext.Courses
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CourseListVm { Courses = coursesQuery };
        }
    }
}
