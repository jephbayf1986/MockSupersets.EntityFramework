using System.Threading;
using System.Threading.Tasks;

namespace MockSupersets.EntityFramework.NetStandard21
{
    public interface IDbContext
    {
        int SaveChanges();

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}