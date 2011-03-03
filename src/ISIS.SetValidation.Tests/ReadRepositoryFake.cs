using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ISIS
{
    public class ReadRepositoryFake : IReadRepository
    {
        private readonly object _queryResult;

        public ReadRepositoryFake(object queryResult)
        {
            _queryResult = queryResult;
        }

        public void Dispose()
        {
        }

        public PageOf<T> All<T>(int pageSize, int pageNumber) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public PageOf<T> All<T>(int pageSize, int pageNumber, params Expression<Func<T, object>>[] orderBys) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public PageOf<T> All<T>(int pageSize, int pageNumber, bool descending, Expression<Func<T, object>> orderBy) where T : class, IEntity
        {
            throw new NotImplementedException();
        }

        public T Single<T>(object id) where T : IEntity
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TElement> Execute<TElement>(IListQuery<TElement> query)
        {
            return (IEnumerable<TElement>) _queryResult;
        }

        public TResult Execute<TResult>(ISingleQuery<TResult> query)
        {
            return (TResult) _queryResult;
        }
    }
}
