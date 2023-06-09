using Application.Common.Exceptions;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Application.Courses.Queries.GetCourseDetails
{
    public class GetCourseDetailsQueryHandler :
        IRequestHandler<GetCourseDetailsQuery, CourseDetailsVm>
    {
        private readonly ICourseDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCourseDetailsQueryHandler(ICourseDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<CourseDetailsVm> Handle(GetCourseDetailsQuery command, CancellationToken cancellationToken)
        {
            var entity = await _dbContext.Courses
                .FirstOrDefaultAsync(course => course.Id == command.Id, cancellationToken);

            //if (entity == null || entity.MentorId != command.MentorId)
            //{
            //    throw new NotFoundException(nameof(Courses), command.MentorId);
            //}
            
            return _mapper.Map<CourseDetailsVm>(entity);
        }
    }
}
