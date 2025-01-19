using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFrameworkCore.Extensions
{
    internal static class MockDbSetExtensions
    {
        public static void VerifyAddedOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.Add(It.Is(match)), Times.Once);
        }

        public static void VerifyAddedNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.Add(It.Is(match)), Times.Never);
        }

        public static void VerifyAddedAsyncOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Once);
        }

        public static void VerifyAddedAsyncNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);
        }

        public static void VerifyRangeAddedOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRange(It.Is(match)), Times.Once);
        }

        public static void VerifyRangeAddedNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRange(It.Is(match)), Times.Never);
        }

        public static void VerifyRangeAddedAsyncOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRangeAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Once);
        }

        public static void VerifyRangeAddedAsyncNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.AddRangeAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);
        }

        public static void VerifyUpdatedOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.Update(It.Is(match)), Times.Once);
        }

        public static void VerifyUpdatedNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.Update(It.Is(match)), Times.Never);
        }

        public static void VerifyRangeUpdatedOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.UpdateRange(It.Is(match)), Times.Once);
        }

        public static void VerifyRangeUpdatedNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.UpdateRange(It.Is(match)), Times.Never);
        }

        public static void VerifyRemovedOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.Remove(It.Is(match)), Times.Once);
        }

        public static void VerifyRemovedNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<T, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.Remove(It.Is(match)), Times.Never);
        }

        public static void VerifyRangeRemovedOnce<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.RemoveRange(It.Is(match)), Times.Once);
        }

        public static void VerifyRangeRemovedNever<T>(this Mock<DbSet<T>> mockDbSet, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            mockDbSet.Verify(x => x.RemoveRange(It.Is(match)), Times.Never);
        }
    }
}