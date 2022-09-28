using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

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