﻿using MockSupersets.EntityFramework.Builders;
using MockSupersets.EntityFramework.Common;
using MockSupersets.EntityFramework.Common.Helpers;
using MockSupersets.EntityFramework.Extensions;
using MockSupersets.EntityFramework.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFramework
{
    public sealed class MockIDbContext<TContext> : IMockDbContextVerifyable, IMockDbContextBuilder<MockIDbContext<TContext>>, IMockObject<TContext> where TContext : class, IDbContext
    {
        private Mock<TContext> _mock;
        private MockDbContextOptions _options;
        private ICollection<MockDbSetBuilder> _dbSetBuilders;

        public MockIDbContext(MockDbContextOptions options = null) : this(new Mock<TContext>(), null, options)
        {
        }

        internal MockIDbContext(Mock<TContext> mock, ICollection<MockDbSetBuilder> mockDbSetBuilders, MockDbContextOptions options = null)
        {
            _mock = mock;

            _options = options ?? new MockDbContextOptions();

            if (mockDbSetBuilders != null)
                _dbSetBuilders = mockDbSetBuilders;
            else
                _dbSetBuilders = ReflectionHelper.GetDbSetBuilders<TContext>(_options);
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

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            var dbSet = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSet.Object.ToList()
                        .ShouldContain(match.Compile());
        }

        public void VerifyNotUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            var dbSet = _mock.GetMockDbSetAttribute<TContext, T>();

            dbSet.Object.ToList()
                        .ShouldNotContain(match.Compile());
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
            _mock.Verify(x => x.SaveChanges(), Times.Once);
        }

        public void VerifyChangesNotSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            try
            {
                _mock.Verify(x => x.SaveChangesAsync(), Times.Once);
            }
            catch
            {
                _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            }
        }

        public void VerifyChangesNotSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(), Times.Never);
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }

        public MockIDbContext<TContext> WithEntities<T>(params T[] items)
            where T : class, new()
        {
            var dbSetBuilder = _dbSetBuilders.GetDbSetFor<T>(_options);

            dbSetBuilder = dbSetBuilder.WithEntities(items);

            return this;
        }

        public MockIDbContext<TContext> WithEntity<T>(params Action<T>[] actions)
            where T : class, new()
        {
            var dbSetBuilder = _dbSetBuilders.GetDbSetFor<T>(_options);

            dbSetBuilder = dbSetBuilder.WithEntity(actions);

            return this;
        }

        public MockIDbContext<TContext> WithActionOnAdd<T>(Action<T> action)
            where T : class, new()
        {
            var dbSetBuilder = _dbSetBuilders.GetDbSetFor<T>(_options);

            dbSetBuilder = dbSetBuilder
                                    .WithRandomData()
                                    .WithCallBackOnAdd(action);

            return this;
        }

        public MockIDbContext<TContext> WithExceptionThrownOnSaveChanges<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChanges())
                 .Throws<TEx>();

            return this;
        }

        public MockIDbContext<TContext> WithExceptionThrownOnSaveChangesAsync<TEx>()
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
                _mock.ApplyDbSetsAsDefaultReturns(_dbSetBuilders);

                return _mock.Object;
            }
        }
    }
}