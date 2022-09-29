using MockSupersets.EntityFramework.Common;
using MockSupersets.EntityFrameworkCore.Builders;
using MockSupersets.EntityFrameworkCore.Extensions;
using MockSupersets.EntityFrameworkCore.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFrameworkCore
{
    public sealed class MockIDbContext<TContext> : IMockDbContextVerifyable, IMockDbContextBuilder, IEFCoreExpansion where TContext : class, IDbContext
    {
        private Mock<TContext> _mock;
        private MockDbContextOptions _options;

        public MockIDbContext(MockDbContextOptions options = null)
        {
            _mock = new Mock<TContext>();

            _options = options ?? new MockDbContextOptions();
        }

        internal MockIDbContext(Mock<TContext> mock, MockDbContextOptions options = null)
        {
            _mock = mock;

            _options = options ?? new MockDbContextOptions();
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match)
           where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyAddedOnce(match);
        }

        public void VerifyNotAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyAddedNever(match);
        }

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyAddedAsyncOnce(match);
        }

        public void VerifyNotAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyAddedAsyncNever(match);
        }

        public void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeAddedOnce(matches);
        }

        public void VerifyRangeNotAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeAddedNever(matches);
        }

        public void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeAddedAsyncOnce(matches);
        }

        public void VerifyRangeNotAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeAddedAsyncNever(matches);
        }

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyUpdatedOnce(match);
        }

        public void VerifyNotUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyUpdatedNever(match);
        }

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeUpdatedOnce(matches);
        }

        public void VerifyRangeNotUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeUpdatedNever(matches);
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRemovedOnce(match);
        }

        public void VerifyNotRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRemovedNever(match);
        }

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeRemovedOnce(match);
        }

        public void VerifyRangeNotRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeRemovedNever(match);
        }

        public void VerifyChangesSaved()
        {
            try
            {
                _mock.Verify(x => x.SaveChanges(), Times.Once);
            }
            catch
            {
                _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), Times.Once);
            }
        }

        public void VerifyChangesNotSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
            _mock.Verify(x => x.SaveChanges(It.IsAny<bool>()), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            try
            {
                _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
            catch
            {
                _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public void VerifyChangesNotSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        public IMockDbContextBuilder WithEntities<T>(params T[] items)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithEntities(items)
                                    .Build();

            _mock.SetReturnsDefault(mockDbSet.Object);

            return this;
        }

        public IMockDbContextBuilder WithEntity<T>(params Action<T>[] actions)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithEntity(actions)
                                    .Build();

            _mock.SetReturnsDefault(mockDbSet.Object);

            return this;
        }

        public IMockDbContextBuilder WithActionOnAdd<T>(Action<T> action)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithRandomData()
                                    .WithCallBackOnAdd(action)
                                    .Build();

            _mock.SetReturnsDefault(mockDbSet.Object);

            return this;
        }

        public IMockDbContextBuilder WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChanges())
                 .Throws<TEx>();

            _mock.Setup(x => x.SaveChanges(It.IsAny<bool>()))
                 .Throws<TEx>();

            return this;
        }

        public IMockDbContextBuilder WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .Throws<TEx>();

            _mock.Setup(x => x.SaveChangesAsync(It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                 .Throws<TEx>();

            return this;
        }
    }
}