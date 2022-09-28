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
    }
}