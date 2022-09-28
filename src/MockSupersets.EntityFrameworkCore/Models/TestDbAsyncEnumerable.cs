using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace MockSupersets.EntityFrameworkCore.Models
{
    internal class TestDbAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
    {
        public TestDbAsyncEnumerable(IEnumerable<T> enumerable)
            : base(enumerable)
        {
        }

        public TestDbAsyncEnumerable(Expression expression)
            : base(expression)
        {
        }

        IQueryProvider IQueryable.Provider => new TestDbAsyncQueryProvider<T>(this);

        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            return new TestDbAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
        }

        public IAsyncEnumerator<T> GetEnumerator()
        {
            return this.GetAsyncEnumerator();
        }
    }
}