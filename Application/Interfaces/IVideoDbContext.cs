using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IVideoDbContext
    {
        DbSet<Video> Videos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
