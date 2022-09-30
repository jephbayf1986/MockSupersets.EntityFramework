using MockSupersets.EntityFramework.Builders;
using MockSupersets.EntityFramework.Helpers;
using Moq;
using System.Collections.Generic;

namespace MockSupersets.EntityFramework.Extensions
{
    internal static class MockExtensions
    {
        public static void ApplyDbSetsAsDefaultReturns<TContext>(this Mock<TContext> mockContext, IEnumerable<MockDbSetBuilder> mockDbSetBuilders)
            where TContext : class
        {
            foreach (var builder in mockDbSetBuilders)
            {
                var mockDbSet = builder.BuildDbSet();

                mockContext.SetReturnsDefault(mockDbSet);
            }
        }
    }
}