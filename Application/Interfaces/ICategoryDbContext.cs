using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ICategoryDbContext
    {
        DbSet<Category> Categories { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
