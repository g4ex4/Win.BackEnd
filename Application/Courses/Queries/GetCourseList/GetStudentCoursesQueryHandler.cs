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
    public class GetStudentCoursesQueryHandler : IRequestHandler<GetStudentCoursesQuery, StudentCourseListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICourseDbContext _dbContext;

        public GetStudentCoursesQueryHandler(IMapper mapper, ICourseDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<StudentCourseListVm> Handle(GetStudentCoursesQuery request, CancellationToken cancellationToken)
        {
            var studentId = request.StudentId;

            var coursesQuery = await _dbContext.Courses
                .Where(c => c.StudentCourse.Any(sc => sc.StudentId == studentId))
                .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new StudentCourseListVm { StudentId = studentId, Courses = coursesQuery };
        }
    }
}
