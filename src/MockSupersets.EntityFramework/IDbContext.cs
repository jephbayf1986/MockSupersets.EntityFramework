using System.Threading;
using System.Threading.Tasks;

namespace MockSupersets.EntityFramework
{
    public interface IDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}