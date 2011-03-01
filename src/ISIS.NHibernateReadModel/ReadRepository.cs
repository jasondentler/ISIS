using System;
using System.Collections.Generic;
using NHibernate;
using System.Linq.Expressions;

namespace ISIS.NHibernateReadModel
{
    public class ReadRepository : IReadRepository
    {
        private readonly ISession _session;

        public ReadRepository(ISession session)
        {
            _session = session;
        }

        public PageOf<T> All<T>(int pageSize, int pageNumber) where T : class, IEntity
        {
            var query = _session.QueryOver<T>();
            return _session.Transact(() => PageQuery(query, pageSize, pageNumber));
        }

        public PageOf<T> All<T>(int pageSize, int pageNumber, params Expression<Func<T, object>>[] orderBys) where T : class, IEntity
        {
            var query = _session.QueryOver<T>();
            foreach (var orderBy in orderBys)
                query = query.OrderBy(orderBy).Asc;
            return _session.Transact(() => PageQuery(query, pageSize, pageNumber));
        }

        public PageOf<T> All<T>(int pageSize, int pageNumber, bool descending, Expression<Func<T, object>> orderBy) where T : class, IEntity
        {
            if (!descending)
                return All(pageSize, pageNumber, orderBy);
            var query = _session.QueryOver<T>()
                .OrderBy(orderBy).Desc;
            return _session.Transact(() => PageQuery(query, pageSize, pageNumber));
        }

        private static PageOf<T> PageQuery<T>(IQueryOver<T, T> query, int pageSize, int pageNumber) where T:class, IEntity
        {
            var skip = (pageNumber - 1)*pageSize;
            var take = pageSize;
            var results = query.Skip(skip).Take(take).Future();
            var count = query.ToRowCountInt64Query().FutureValue<long>();
            return new PageOf<T>(count.Value, pageNumber, pageSize, results);
        }

        public T Single<T>(object id) where T : IEntity
        {
            var retval = _session.Transact(() => _session.Get<T>(id));
            return retval;
        }

        public IEnumerable<TElement> Execute<TElement>(IListQuery<TElement> query)
        {
            return BuildQuery(query).List<TElement>();
        }

        public TResult Execute<TResult>(ISingleQuery<TResult> query)
        {
            return BuildQuery(query).UniqueResult<TResult>();
        }

        private NHibernate.IQuery BuildQuery(IQuery query)
        {
            var nhQuery = _session.GetNamedQuery(query.QueryName);
            foreach (var param in query.GetParameters())
                nhQuery.SetParameter(param.Key, param.Value);
            return nhQuery;
        }

        public void Dispose()
        {
            _session.Dispose();
        }

    }
}
