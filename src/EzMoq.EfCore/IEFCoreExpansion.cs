using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MockSupersets.EntityFrameworkCore
{
    public interface IEFCoreExpansion
    {
        void VerifyAddedAsync<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyNotAddedAsync<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new();

        void VerifyRangeNotAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new();

        void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new();

        void VerifyRangeNotUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new();
    }
}