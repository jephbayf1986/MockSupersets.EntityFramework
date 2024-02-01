using MockSupersets.EntityFramework.Builders;
using MockSupersets.EntityFramework.Common;
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

        public static MockDbSetBuilder<T> GetDbSetBuilder<TContext, T>(this Mock<TContext> mockContext, MockDbContextOptions options)
            where T : class, new()
            where TContext : class
        {
            var existingDbSet = mockContext.GetMockDbSetAttribute<TContext, T>();

            if (existingDbSet == null)
                return new MockDbSetBuilder<T>(options);
            else
                return new MockDbSetBuilder<T>(existingDbSet, options);
        }
    }
}