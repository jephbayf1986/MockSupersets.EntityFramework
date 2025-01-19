using System.Collections.Generic;
using System.Linq.Expressions;
using System;
using Moq;

namespace EzMoq.EfCore.Interfaces
{
    public interface IVerifyActions
    {
        void VerifyAddedOnce<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyAdded<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        void VerifyNeverAdded<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyAddedOnceAsync<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyAddedAsync<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();
        
        void VerifyNeverAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyRangeAddedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new();

        void VerifyRangeNeverAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        void VerifyRangeAddedOnceAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new();

        void VerifyRangeNeverAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        void VerifyUpdatedOnce<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyUpdated<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        void VerifyNeverUpdated<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyRangeUpdatedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new();

        void VerifyRangeNeverUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new();

        void VerifyRemovedOnce<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyRemoved<T>(Expression<Func<T, bool>> match, Times times) where T : class, new();

        void VerifyNeverRemoved<T>(Expression<Func<T, bool>> match) where T : class, new();

        void VerifyRangeRemovedOnce<T>(Expression<Func<IEnumerable<T>, bool>> match) where T : class, new();

        void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match, Times times) where T : class, new();

        void VerifyRangeNeverRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match) where T : class, new();
    }
}