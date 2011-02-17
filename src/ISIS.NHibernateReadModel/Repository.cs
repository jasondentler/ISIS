using System;
using NHibernate;
using System.Linq.Expressions;

namespace ISIS.NHibernateReadModel
{
    public class Repository : IRepository
    {
        private readonly ISession _session;

        public Repository(ISession session)
        {
            _session = session;
        }

        public void Insert<T>(T entity) where T : IEntity
        {
            _session.Transact(() => _session.Save(entity));
        }

        public void Update<T>(T entity) where T : IEntity
        {
            _session.Transact(() => _session.Update(entity));
        }

        public void Delete<T>(T entity) where T : IEntity
        {
            _session.Transact(() => _session.Delete(entity));
        }

        public void Delete<T, TId>(TId id) where T : IEntity
        {
            _session.Transact(() =>
                                  {
                                      var entity = _session.Load<T>(id);
                                      _session.Delete(entity);
                                  });
        }

        public PageOf<T> All<T>(int pageSize, long pageNumber) where T : class, IEntity
        {
            var query = _session.QueryOver<T>();
            return _session.Transact(() => PageQuery(query, pageSize, pageNumber));
        }

        public PageOf<T> All<T>(int pageSize, long pageNumber, params Expression<Func<T, object>>[] orderBys) where T : class, IEntity
        {
            var query = _session.QueryOver<T>();
            foreach (var orderBy in orderBys)
                query = query.OrderBy(orderBy).Asc;
            return _session.Transact(() => PageQuery(query, pageSize, pageNumber));
        }

        public PageOf<T> All<T>(int pageSize, long pageNumber, bool descending, Expression<Func<T, object>> orderBy) where T : class, IEntity
        {
            if (!descending)
                return All(pageSize, pageNumber, orderBy);
            var query = _session.QueryOver<T>()
                .OrderBy(orderBy).Desc;
            return _session.Transact(() => PageQuery(query, pageSize, pageNumber));
        }

        private static PageOf<T> PageQuery<T>(IQueryOver<T, T> query, int pageSize, long pageNumber) where T:class, IEntity
        {
            var results = query.Future();
            var count = query.ToRowCountInt64Query().FutureValue<long>();
            return new PageOf<T>(count.Value, pageNumber, pageSize, results);
        }

        public T Single<T>(object id) where T : IEntity
        {
            return _session.Transact(() => _session.Get<T>(id));
        }

        public void Dispose()
        {
            _session.Dispose();
        }

    }
}
