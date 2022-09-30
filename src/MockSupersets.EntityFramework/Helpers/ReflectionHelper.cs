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
                                                   .Where(x => x.PropertyType == typeof(DbSet<>));

            var builders = new List<MockDbSetBuilder>();

            foreach (var dbSetProperty in dbSetProperties)
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
            var dbSetGenericType = dbSetProperty.GetType().GetGenericArguments()[0];

            var builderBaseType = typeof(MockDbSetBuilder<>);

            var builderType = builderBaseType.MakeGenericType(dbSetGenericType);

            var newBuilder = Activator.CreateInstance(builderType, options);

            // Trigger Random Data Method

            return (MockDbSetBuilder)newBuilder;
        }
    }
}