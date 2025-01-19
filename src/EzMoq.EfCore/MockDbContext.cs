using EzMoq.EfCore.Builders;
using EzMoq.EfCore.Interfaces;
using EzMoq.EfCore.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;

namespace EzMoq.EfCore
{
    public sealed class MockDbContext<TContext> : IVerifyActions, IVerifySave where TContext : class, IDbContext
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

        public void VerifyAddedOnce<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.Add(It.Is(match)), Times.Once);
        }

        public void VerifyAdded<T>(Expression<Func<T, bool>> match, Times times)
            where T : class, new()
        {
            _mock.Verify(x => x.Add(It.Is(match)), times);
        }

        public void VerifyNeverAdded<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.Add(It.Is(match)), Times.Never);
        }

        public void VerifyAddedOnceAsync<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
            _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyAddedAsync<T>(Expression<Func<T, bool>> match, Times times) 
            where T : class, new()
        {
            _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), times);
        }

        public void VerifyNeverAddedAsync<T>(Expression<Func<T, bool>> match) 
            where T : class, new()
        {
            _mock.Verify(x => x.AddAsync(It.Is(match), It.IsAny<CancellationToken>()), Times.Never);
        }

        public void VerifyRangeAddedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            _mock.Verify(x => x.AddRange(It.Is(matches)), Times.Once);
        }

        public void VerifyRangeAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new()
        {
            _mock.Verify(x => x.AddRange(It.Is(matches)), times);
        }

        public void VerifyRangeNeverAdded<T>(Expression<Func<IEnumerable<T>, bool>> matches)
            where T : class, new()
        {
            _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), Times.Never);
        }

        public void VerifyRangeAddedOnceAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyRangeAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new()
        {
            _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), times);
        }

        public void VerifyRangeNeverAddedAsync<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            _mock.Verify(x => x.AddRangeAsync(It.Is(matches), It.IsAny<CancellationToken>()), Times.Never);
        }

        public void VerifyUpdatedOnce<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.Update(It.Is(match)), Times.Once);
        }

        public void VerifyUpdated<T>(Expression<Func<T, bool>> match, Times times) where T : class, new()
        {
            _mock.Verify(x => x.Update(It.Is(match)), times);
        }

        public void VerifyNeverUpdated<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.Update(It.Is(match)), Times.Never);
        }

        public void VerifyRangeUpdatedOnce<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            _mock.Verify(x => x.UpdateRange(It.Is(matches)), Times.Once);
        }

        public void VerifyRangeUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches, Times times) where T : class, new()
        {
            _mock.Verify(x => x.UpdateRange(It.Is(matches)), times);
        }

        public void VerifyRangeNeverUpdated<T>(Expression<Func<IEnumerable<T>, bool>> matches) 
            where T : class, new()
        {
            _mock.Verify(x => x.UpdateRange(It.Is(matches)), Times.Never);
        }

        public void VerifyRemovedOnce<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.Remove(It.Is(match)), Times.Once);
        }

        public void VerifyRemoved<T>(Expression<Func<T, bool>> match, Times times) where T : class, new()
        {
            _mock.Verify(x => x.Remove(It.Is(match)), times);
        }

        public void VerifyNeverRemoved<T>(Expression<Func<T, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.Remove(It.Is(match)), Times.Never);
        }

        public void VerifyRangeRemovedOnce<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.RemoveRange(It.Is(match)), Times.Once);
        }

        public void VerifyRangeRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match, Times times) where T : class, new()
        {
            _mock.Verify(x => x.RemoveRange(It.Is(match)), times);
        }

        public void VerifyRangeNeverRemoved<T>(Expression<Func<IEnumerable<T>, bool>> match)
            where T : class, new()
        {
            _mock.Verify(x => x.RemoveRange(It.Is(match)), Times.Never);
        }

        public void VerifyChangesSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Once);
        }

        public void VerifyChangesNeverSaved()
        {
            _mock.Verify(x => x.SaveChanges(), Times.Never);
        }

        public void VerifyChangesSavedAsync()
        {
            _mock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        public void VerifyChangesNeverSavedAsync()
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