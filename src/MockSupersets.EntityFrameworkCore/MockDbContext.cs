using MockSupersets.EntityFrameworkCore.Builders;
using MockSupersets.EntityFrameworkCore.Extensions;
using MockSupersets.EntityFrameworkCore.Helpers;
using MockSupersets.EntityFrameworkCore.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFrameworkCore
{
    public sealed class MockDbContext<TContext> : IEFCoreExpansion where TContext : class, IDbContext
    {
        private readonly Mock<TContext> _mock;
        private readonly MockDbContextOptions _options;

        public MockDbContext(MockDbContextOptions options = null)
        {
            _mock = new Mock<TContext>();

            _options = options ?? new MockDbContextOptions();
        }

        internal MockDbContext(Mock<TContext> mock, MockDbContextOptions options = null)
        {
            _mock = mock;

            _options = options ?? new MockDbContextOptions();
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            try
            {
                _mock.GetMockDbSetAttribute<TContext, T>()
                     .VerifyAddedOnce(match);
            }
            catch
            {
                _mock.VerifyAddedOnce(match);
            }
        }

        public void VerifyNotAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyAddedNever(match);

            _mock.VerifyAddedNever(match);
        }

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new()
        {
            try
            {
                _mock.GetMockDbSetAttribute<TContext, T>()
                     .VerifyAddedAsyncOnce(match);
            }
            catch
            {
                _mock.VerifyAddedAsyncOnce(match);
            }
        }

        public void VerifyNotAddedAsync<T>(Expression<Func<T, bool>> match) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyAddedAsyncNever(match);

            _mock.VerifyAddedAsyncNever(match);
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
            try
            {
                _mock.GetMockDbSetAttribute<TContext, T>()
                     .VerifyUpdatedOnce(match);
            }
            catch
            {
                _mock.VerifyUpdatedOnce(match);
            }
        }

        public void VerifyNotUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyUpdatedNever(match);

            _mock.VerifyUpdatedNever(match);
        }

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new()
        {
            try
            {
                _mock.GetMockDbSetAttribute<TContext, T>()
                     .VerifyRangeUpdatedOnce(matches);
            }
            catch
            {
                _mock.VerifyRangeUpdatedOnce(matches);
            }
        }

        public void VerifyRangeNotUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeUpdatedNever(matches);

            _mock.VerifyRangeUpdatedNever(matches);
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            try
            {
                _mock.GetMockDbSetAttribute<TContext, T>()
                     .VerifyRemovedOnce(match);
            }
            catch
            {
                _mock.VerifyRemovedOnce(match);
            }
        }

        public void VerifyNotRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRemovedNever(match);

            _mock.VerifyRemovedNever(match);
        }

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            try
            {
                _mock.GetMockDbSetAttribute<TContext, T>()
                     .VerifyRangeRemovedOnce(match);
            }
            catch
            {
                _mock.VerifyRangeRemovedOnce(match);
            }
        }

        public void VerifyRangeNotRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            _mock.GetMockDbSetAttribute<TContext, T>()
                 .VerifyRangeRemovedNever(match);

            _mock.VerifyRangeRemovedNever(match);
        }

        public void VerifyChangesNotSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Once);
        }

        public void VerifyChangesSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyChangesNotSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        public MockDbContext<TContext> WithEntities<T>(params T[] items)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithEntities(items)
                                    .Build();

            _mock.SetReturnsDefault(mockDbSet.Object);

            return this;
        }

        public MockDbContext<TContext> WithEntity<T>(params Action<T>[] actions)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithEntity(actions)
                                    .Build();

            _mock.SetReturnsDefault(mockDbSet.Object);

            return this;
        }

        public MockDbContext<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new()
        {
            var mockDbSet = new MockDbSetBuilder<T>(_options)
                                    .WithRandomData()
                                    .WithCallBackOnAdd(action)
                                    .Build();

            _mock.SetReturnsDefault(mockDbSet.Object);

            return this;
        }

        public MockDbContext<TContext> WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChanges())
                 .Throws<TEx>();

            return this;
        }

        public MockDbContext<TContext> WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .Throws<TEx>();

            return this;
        }

        public TContext Object
        {
            get
            {
                return _mock.Object;
            }
        }
    }
}