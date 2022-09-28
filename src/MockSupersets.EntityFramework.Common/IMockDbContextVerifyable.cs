using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFramework.Common
{
    public interface IMockDbContextVerifyable
    {
        void VerifyAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new();

        void VerifyNotAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyRangeNotAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new();

        void VerifyUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyNotUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyNotRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new();

        void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new();

        void VerifyRangeNotRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new();

        void VerifyChangesSaved();

        void VerifyChangesNotSaved();

        void VerifyChangesSavedAsync();

        void VerifyChangesNotSavedAsync();
    }
}