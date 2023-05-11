using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ICourseDbContext
    {
        DbSet<Course> Courses { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
