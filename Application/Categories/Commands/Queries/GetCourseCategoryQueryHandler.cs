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
    public class GetCourseCategoryQueryHandler : IRequestHandler<GetCourseCategoryQuery, CategoryListVm>
    {
        private readonly IMapper _mapper;
        private readonly ICategoryDbContext _dbContext;
        private readonly ILogger<GetCourseCategoryQueryHandler> _logger;

        public GetCourseCategoryQueryHandler(IMapper mapper, ICategoryDbContext dbContext,
            ILogger<GetCourseCategoryQueryHandler> logger)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<CategoryListVm> Handle(GetCourseCategoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var categoryQuery = await _dbContext.Categories.Where(category => category.Id == request.CategoryId)
                    .ProjectTo<CategoryLookupDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                return new CategoryListVm { Categories = categoryQuery };
            }
            catch (Exception ex)
            {
                _logger.LogError($" {ex}");
                throw new Exception($"An error occurred while getting course information: {ex}");
            }
        }
    }
}
