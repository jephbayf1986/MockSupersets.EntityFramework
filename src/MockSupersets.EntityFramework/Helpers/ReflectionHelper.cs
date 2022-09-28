using Moq;
using System.Data.Entity;
using System.Linq;

namespace MockSupersets.EntityFramework.Helpers
{
    internal static class ReflectionHelper
    {
        public static Mock<DbSet<T>> GetMockDbSetAttribute<TContext, T>(this Mock<TContext> mockContext)
            where T : class
            where TContext : class
        {
            var propertyMatching = typeof(TContext).GetProperties()
                                                   .FirstOrDefault(x => x.PropertyType == typeof(DbSet<T>));

            var dbSet = propertyMatching.GetValue(mockContext.Object, null);

            if (dbSet == null) return null;

            return (dbSet as DbSet<T>).GetMockFromObject();
        }
    }
}