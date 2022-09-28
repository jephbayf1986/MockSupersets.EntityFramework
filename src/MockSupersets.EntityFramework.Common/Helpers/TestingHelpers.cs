using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MockSupersets.EntityFramework.Common.Helpers
{
    internal static class TestingHelpers
    {
        public static void ShouldContain<T>(this IEnumerable<T> values, Func<T, bool> predicate)
        {
            var hasMatch = values.Any(x => predicate(x));

            Assert.True(hasMatch);
        }

        public static void ShouldNotContain<T>(this IEnumerable<T> values, Func<T, bool> predicate)
        {
            var hasMatch = values.Any(x => predicate(x));

            Assert.False(hasMatch);
        }
    }
}