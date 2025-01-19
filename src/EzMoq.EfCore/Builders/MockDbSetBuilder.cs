using dotRandom;
using Microsoft.EntityFrameworkCore;
using MockSupersets.EntityFrameworkCore.Models;
using MockSupersets.EntityFrameworkCore.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace MockSupersets.EntityFrameworkCore.Builders
{
    internal sealed class MockDbSetBuilder<T>
        where T : class, new()
    {
        private Mock<DbSet<T>> _mock;
        private readonly MockDbContextOptions _options;

        public MockDbSetBuilder(MockDbContextOptions options)
        {
            _mock = new Mock<DbSet<T>>();
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
            ICollection<T> items = new List<T>();

            for (var i = 0; i < _options.MinItemsInDbSet; i++)
                items.Add(DotRandom.GenerateRandom<T>());

            SetDbSetData(items);

            return this;
        }

        public MockDbSetBuilder<T> WithEntities(params T[] entities)
        {
            ICollection<T> items = entities.ToList();

            // Fill remaining quota with random data
            if (items.Count() < _options.MinItemsInDbSet)
            {
                var startPoint = items.Count() - 1;

                for (var i = startPoint; i < _options.MinItemsInDbSet; i++)
                    items.Add(DotRandom.GenerateRandom<T>());
            }

            SetDbSetData(items);

            return this;
        }

        public MockDbSetBuilder<T> WithEntity(params Action<T>[] actions)
        {
            ICollection<T> items = new List<T>();

            var numberOfItems = _options.MinItemsInDbSet;
            var itemToAction = DotRandom.RandomIntBetween(0, numberOfItems - 1);

            for (var i = 0; i < numberOfItems; i++)
            {
                var item = DotRandom.GenerateRandom<T>();

                if (i == itemToAction)
                {
                    foreach (var action in actions)
                        action(item);
                }

                items.Add(item);
            }

            SetDbSetData(items);

            return this;
        }

        private void SetDbSetData(IEnumerable<T> data)
        {
            var queryableData = data.AsQueryable();

            _mock.As<IAsyncEnumerable<T>>()
               .Setup(m => m.GetAsyncEnumerator(CancellationToken.None))
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
            return _mock;
        }
    }
}