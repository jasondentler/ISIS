using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ISIS
{

    public interface IReadRepository : IDisposable
    {

        PageOf<T> All<T>(int pageSize, long pageNumber) 
            where T : class, IEntity;

        PageOf<T> All<T>(int pageSize, long pageNumber, params Expression<Func<T, object>>[] orderBys)
            where T : class, IEntity;

        PageOf<T> All<T>(int pageSize, long pageNumber, bool descending, Expression<Func<T, object>> orderBy)
            where T : class, IEntity;

        T Single<T>(object id) where T : IEntity;

        IEnumerable<TElement> Execute<TElement>(IListQuery<TElement> query);
        TResult Execute<TResult>(ISingleQuery<TResult> query);

    }

}
