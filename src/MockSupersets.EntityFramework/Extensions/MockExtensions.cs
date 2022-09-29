using MockSupersets.EntityFramework.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace MockSupersets.EntityFramework.Extensions
{
    internal static class MockExtensions
    {
        public static void ApplyDbSetsAsDefaultReturns<TContext>(this TContext context, IEnumerable<MockDbSetBuilder> mockDbSetBuilders)
        {

        }
    }
}