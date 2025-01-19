using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Reflection;

namespace MockSupersets.EntityFrameworkCore.Helpers
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

        public static Mock<T> GetMockFromObject<T>(this T mockedObject) where T : class
        {
            PropertyInfo[] propInfo = mockedObject.GetType().GetProperties()
                .Where(
                    p => p.PropertyType.Name == "Mock`1"
                ).ToArray();

            return propInfo.FirstOrDefault().GetGetMethod().Invoke(mockedObject, null) as Mock<T>;
        }
    }
}