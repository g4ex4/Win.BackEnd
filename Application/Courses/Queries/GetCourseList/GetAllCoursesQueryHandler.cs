using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<GetAllCoursesQueryHandler> _logger;

        public GetAllCoursesQueryHandler(IMapper mapper, ICourseDbContext dbContext,
            ILogger<GetAllCoursesQueryHandler> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CourseListVm> Handle(GetAllCoursesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var coursesQuery = await _dbContext.Courses
                    .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new CourseListVm { Courses = coursesQuery };
            }
            catch (Exception ex)
            {
                _logger.LogError($" {ex}");
                throw new Exception($"An error occurred while getting course information: { ex}");
            }
        }
    }
}
