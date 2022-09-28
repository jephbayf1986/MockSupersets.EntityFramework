using MockSupersets.EntityFramework.Builders;
using MockSupersets.EntityFramework.Extensions;
using MockSupersets.EntityFramework.Helpers;
using MockSupersets.EntityFramework.Shared;
using MockSupersets.EntityFramework.Shared.Helpers;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFramework
{
    public sealed class MockDbContext<TContext> : IMockDbContextVerifyable, IMockDbContextBuilder where TContext : DbContext
    {
        private Mock<TContext> _mock;
        private MockDbContextOptions _options;
        
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

        public void VerifyChangesNotSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Once);
        }

        public void VerifyChangesSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
        }

        public void VerifyChangesSavedAsync(CancellationToken? cancellationToken = null)
        {
            if (cancellationToken.HasValue)
                _mock.Verify(x => x.SaveChangesAsync(cancellationToken.Value), Times.Once);
            else
                _mock.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        public void VerifyChangesNotSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(), Times.Never);
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
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

            return this;
        }

        public IMockDbContextBuilder WithExceptionThrownOnSaveChangesAsync<TEx>()
            where TEx : Exception, new()
        {
            _mock.Setup(x => x.SaveChangesAsync())
                 .Throws<TEx>();

            _mock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                 .Throws<TEx>();

            return this;
        }
    }
}