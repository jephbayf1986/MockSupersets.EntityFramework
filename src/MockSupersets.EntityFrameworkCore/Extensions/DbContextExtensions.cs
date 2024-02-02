using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFrameworkCore.Extensions
{
    internal static class DbContextExtensions
    {
        public static void VerifyAddedOnce<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.Add(It.Is(match)), Times.Once);
        }

        public static void VerifyAddedNever<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.Add(It.Is(match)), Times.Never);
        }

        public static void VerifyAddedAsyncOnce<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Once);
        }

        public static void VerifyAddedAsyncNever<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);
        }

        public static void VerifyUpdatedOnce<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.Update(It.Is(match)), Times.Once);
        }

        public static void VerifyUpdatedNever<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.Update(It.Is(match)), Times.Never);
        }

        public static void VerifyRangeUpdatedOnce<TContext, T>(this Mock<TContext> mockContext, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.UpdateRange(It.Is(match)), Times.Once);
        }

        public static void VerifyRangeUpdatedNever<TContext, T>(this Mock<TContext> mockContext, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.UpdateRange(It.Is(match)), Times.Never);
        }

        public static void VerifyRemovedOnce<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.Remove(It.Is(match)), Times.Once);
        }

        public static void VerifyRemovedNever<TContext, T>(this Mock<TContext> mockContext, Expression<Func<T, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.Remove(It.Is(match)), Times.Never);
        }

        public static void VerifyRangeRemovedOnce<TContext, T>(this Mock<TContext> mockContext, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.RemoveRange(It.Is(match)), Times.Once);
        }

        public static void VerifyRangeRemovedNever<TContext, T>(this Mock<TContext> mockContext, Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
            where TContext : class, IDbContext
        {
            mockContext.Verify(x => x.RemoveRange(It.Is(match)), Times.Never);
        }
    }
}
