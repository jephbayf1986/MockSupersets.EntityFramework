using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace MockSupersets.EntityFrameworkCore
{
    public interface IDbContext
    {
        int SaveChanges();

        int SaveChanges(bool acceptAllChangesOnSuccess);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Add<TEntity>(TEntity entity)
            where TEntity : class;

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;

        EntityEntry<TEntity> Update<TEntity>(TEntity entity)
            where TEntity : class;

        void UpdateRange(params object[] entities);

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
            where TEntity : class;

        void RemoveRange(params object[] entities);
    }
}