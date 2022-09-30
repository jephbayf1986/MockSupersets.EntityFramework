using MockSupersets.EntityFramework.Builders;
using MockSupersets.EntityFramework.Common;
using MockSupersets.EntityFramework.Common.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace MockSupersets.EntityFramework.Helpers
{
    internal static class ReflectionHelper
    {
        public static ICollection<MockDbSetBuilder> GetDbSetBuilders<TContext>(MockDbContextOptions options)
            where TContext : class
        {
            if (!options.AutoPopulateDbSets)
                return new List<MockDbSetBuilder>();

            var dbSetProperties = typeof(TContext).GetProperties()
                                                   .Where(x => x.PropertyType.Name == typeof(DbSet<>).Name);

            var builders = new List<MockDbSetBuilder>();

            // Ensure only 1 for each generic type
            var uniqueDbSetProperties = dbSetProperties.GroupBy(x => x.GetDbSetGenericType())
                                                       .Select(x => x.First());

            foreach (var dbSetProperty in uniqueDbSetProperties)
                builders.Add(dbSetProperty.CreateDbSetBuilder(options));

            return builders;
        }

        public static MockDbSetBuilder<T> GetDbSetFor<T>(this ICollection<MockDbSetBuilder> builders, MockDbContextOptions options)
            where T : class, new()
        {
            var existingBuilder = builders.FirstOrDefault(x => x.GetType() == typeof(MockDbSetBuilder<T>));

            if (existingBuilder == null)
            {
                builders.Add(new MockDbSetBuilder<T>(options));
            }
                
            return (MockDbSetBuilder<T>)builders.First(x => x.GetType() == typeof(MockDbSetBuilder<T>));
        }

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

        private static MockDbSetBuilder CreateDbSetBuilder(this PropertyInfo dbSetProperty, MockDbContextOptions options)
        {
            var dbSetGenericType = dbSetProperty.GetDbSetGenericType();

            var builderBaseType = typeof(MockDbSetBuilder<>);

            var builderType = builderBaseType.MakeGenericType(dbSetGenericType);

            var emptyBuilder = Activator.CreateInstance(builderType, options);

            var withRandomDataMethod = builderType.GetMethod("WithRandomData");

            var builderWithRandomData = withRandomDataMethod.Invoke(emptyBuilder, new object[0]);

            return (MockDbSetBuilder)builderWithRandomData;
        }

        private static Type GetDbSetGenericType(this PropertyInfo dbSetProperty)
            => dbSetProperty.PropertyType.GetGenericArguments()[0];
    }
}