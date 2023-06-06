using Application.Courses.Queries.GetCourseList;
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

namespace Application.Categories.Commands.Queries
{
    public class GetCourseCategoryQueryHandler : IRequestHandler<GetCourseCategoryQuery, CourseListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICourseDbContext _dbContext;
        private readonly ILogger<GetCourseCategoryQueryHandler> _logger;

        public GetCourseCategoryQueryHandler(IMapper mapper, ICourseDbContext dbContext,
            ILogger<GetCourseCategoryQueryHandler> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CourseListVm> Handle(GetCourseCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var coursesQuery = await _dbContext.Courses.Where(course => course.CategoryId == request.CategoryId)
                    .ProjectTo<CourseLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new CourseListVm { Courses = coursesQuery };
            }
            catch (Exception ex)
            {
                _logger.LogError($" {ex}");
                throw new Exception($"An error occurred while getting course information: {ex}");
            }
        }
    }
}
