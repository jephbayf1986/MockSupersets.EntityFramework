﻿using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

#if NET5_0_OR_GREATER
using Microsoft.EntityFrameworkCore.Query;
#else
using Microsoft.EntityFrameworkCore.Query.Internal;
#endif

namespace EzMoq.EfCore.Models
{
    public class TestDbAsyncQueryProvider<TEntity> : IAsyncQueryProvider
    {
        private readonly IQueryProvider innerQueryProvider;

        public TestDbAsyncQueryProvider(IQueryProvider innerQueryProvider)
        {
            this.innerQueryProvider = innerQueryProvider;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return new TestDbAsyncEnumerable<TEntity>(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new TestDbAsyncEnumerable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return innerQueryProvider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return innerQueryProvider.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = new CancellationToken())
        {
            var result = Execute(expression);

            var expectedResultType = typeof(TResult).GetGenericArguments()?.FirstOrDefault();
            if (expectedResultType == null)
            {
                return default;
            }

            return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(expectedResultType)
                .Invoke(null, new[] { result });
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            return Task.FromResult(Execute(expression));
        }
    }
}