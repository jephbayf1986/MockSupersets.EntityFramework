using dotRandom;
using MockSupersets.EntityFramework.Common;
using MockSupersets.EntityFramework.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace MockSupersets.EntityFramework.Builders
{
    internal abstract class MockDbSetBuilder
    {
    }

    internal sealed class MockDbSetBuilder<T> : MockDbSetBuilder
        where T : class, new()
    {
        private Mock<DbSet<T>> _mock;
        private readonly MockDbContextOptions _options;
        private readonly ICollection<T> _items;

        public MockDbSetBuilder(MockDbContextOptions options)
        {
            _mock = new Mock<DbSet<T>>();

            _items = new List<T>();

            _options = options;
        }

        public MockDbSetBuilder<T> WithCallBackOnAdd(Action<T> action)
        {
            _mock.Setup(x => x.Add(It.IsAny<T>()))
                 .Callback((T newItem) => action(newItem));

            return this;
        }

        public MockDbSetBuilder<T> WithRandomData()
        {
            for (var i = 0; i < _options.MinItemsInDbSet; i++)
                _items.Add(DotRandom.GenerateRandom<T>());

            return this;
        }

        public MockDbSetBuilder<T> WithEntities(params T[] entities) 
        {
            foreach (var entity in entities)
                _items.Add(entity);

            // Fill remaining quota with random data
            if (_items.Count() < _options.MinItemsInDbSet)
            {
                var startPoint = _items.Count() - 1;

                for (var i = startPoint; i < _options.MinItemsInDbSet; i++)
                    _items.Add(DotRandom.GenerateRandom<T>());
            }

            return this;
        }

        public MockDbSetBuilder<T> WithEntity(params Action<T>[] actions)
        {
            var minimumQuotaMet = _items.Count >= _options.MinItemsInDbSet;

            var itemsToCreate = minimumQuotaMet ? 1 : _options.MinItemsInDbSet - _items.Count;
            var itemToAction = DotRandom.RandomIntBetween(0, itemsToCreate - 1);

            for (var i = 0; i < itemsToCreate; i++)
            {
                var item = DotRandom.GenerateRandom<T>();

                if (i == itemToAction)
                {
                    foreach (var action in actions)
                        action(item);
                }

                _items.Add(item);
            }

            return this;
        }

        private void SetDbSetData(IEnumerable<T> data)
        {
            var queryableData = data.AsQueryable();

            _mock.As<IDbAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new TestDbAsyncEnumerator<T>(queryableData.GetEnumerator()));

            _mock.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new TestDbAsyncQueryProvider<T>(queryableData.Provider));

            _mock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
            _mock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
            _mock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryableData.GetEnumerator());
        }

        public Mock<DbSet<T>> Build()
        {
            SetDbSetData(_items);

            return _mock;
        }
    }
}