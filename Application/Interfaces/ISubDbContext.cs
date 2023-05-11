using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface ISubDbContext
    {
        DbSet<Subscription> Subs { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
