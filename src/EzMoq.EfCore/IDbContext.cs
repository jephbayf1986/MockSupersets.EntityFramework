using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EzMoq.EfCore
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

        void AddRange(params object[] entities);

        ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
            where TEntity : class;

        Task AddRangeAsync(IEnumerable<object> entities, CancellationToken cancellationToken = default);

        Task AddRangeAsync(params object[] entities);

        EntityEntry<TEntity> Update<TEntity>(TEntity entity)
            where TEntity : class;

        void UpdateRange(params object[] entities);

        EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
            where TEntity : class;

        void RemoveRange(params object[] entities);
    }
}