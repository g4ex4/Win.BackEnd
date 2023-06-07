using Application.Interfaces;
using Domain.Common;
using Domain.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Empl.Commands.DeleteCommands
{
    public class DeleteEmplCommandHandler : IRequestHandler<DeleteEmplCommand, Response>
    {
        private readonly IEmployeeDbContext _dbContext;
        private readonly ICourseDbContext _coursedbContext;

        public DeleteEmplCommandHandler(IEmployeeDbContext dbContext, ICourseDbContext coursedbContext)
        {
            _dbContext = dbContext;
            _coursedbContext = coursedbContext;
        }

        public async Task<Response> Handle(DeleteEmplCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var entity = await _dbContext.Employees.FirstOrDefaultAsync(x => x.Id == request.EmployeeId);
                if (entity == null)
                {
                    return new Response(400, "Employee not found", false);
                }
                if (entity.Email == "1goldyshsergei1@gmail.com")
                {
                    return new Response(400, "This user cannot be deleted", false);
                }
                var mentorCourse = await _coursedbContext.Courses.FirstOrDefaultAsync(c => c.MentorId == request.EmployeeId);
                if (mentorCourse != null)
                {
                    return new Response(400, "The mentor has active courses. Delete active mentor courses first", false);
                }

                _dbContext.Employees.Remove(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return new Response(200, "Employee deleted successfully", true);
            }
            catch (Exception ex)
            {
                return new Response(403, ex.Message, true);
            }
        }
    }
}
