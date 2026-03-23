using Microsoft.EntityFrameworkCore;

namespace Domain.Interfaces;

public interface IBookingDbContext : IDisposable
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
