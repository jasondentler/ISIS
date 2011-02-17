using System;

namespace ISIS
{
    public interface IRepository : IReadRepository, IDisposable 
    {
        void Insert<T>(T entity) where T : IEntity;
        void Update<T>(T entity) where T : IEntity;
        void Delete<T>(T entity) where T : IEntity;
        void Delete<T, TId>(TId id) where T : IEntity;

    }
}
