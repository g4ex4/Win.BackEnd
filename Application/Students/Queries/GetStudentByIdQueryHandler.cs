﻿using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Students.Queries
{
    public class GetStudentByIdQueryHandler : IRequestHandler<GetStudentByIdQuery, StudentLookupDto>
    {
        private readonly IMapper _mapper;
        private readonly IStudentDbContext _dbContext;

        public GetStudentByIdQueryHandler(IMapper mapper, IStudentDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<StudentLookupDto> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var student = await _dbContext.Students
                .Where(s => s.Id == request.StudentId)
                .Select(s => new StudentLookupDto
                {
                    Id = s.Id,
                    UserName = s.UserName,
                    Email = s.Email,
                    DateTimeAdded = s.DateTimeAdded,
                    DateTimeUpdated = s.DateTimeUpdated
                })
                .FirstOrDefaultAsync(cancellationToken);

            return student;
        }
    }
}
