using MockSupersets.EntityFramework.Builders;
using MockSupersets.EntityFramework.Common;
using MockSupersets.EntityFramework.Common.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MockSupersets.EntityFramework.Helpers
{
    internal static class ReflectionHelper
    {
        public static ICollection<MockDbSetBuilder> AutoPopulateDbSets<TContext>(MockDbContextOptions options)
            where TContext : class
        {
            var dbSetProperties = typeof(TContext).GetProperties()
                                                   .Where(x => x.PropertyType == typeof(DbSet<>));

            var builders = new List<MockDbSetBuilder>();

            foreach (var dbSetProperty in dbSetProperties)
            {
                var dbSetGenericType = dbSetProperty.GetType().GetGenericArguments()[0];

                var builderBaseType = typeof(MockDbSetBuilder<>);

                var builderType = builderBaseType.MakeGenericType(dbSetGenericType);

                var newBuilder = Activator.CreateInstance(builderType, options);



                builders.Add((MockDbSetBuilder)newBuilder);
            }

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
    }
}