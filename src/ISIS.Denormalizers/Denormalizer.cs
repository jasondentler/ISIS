using System;
using System.Configuration;
using System.Data;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{

    public abstract class Denormalizer<TEntity> : IDenormalizer
        where TEntity : IEntity
    {

        private readonly Func<IDbConnection> _connectionFactory;
        protected IDialect Db { get; private set; }

        protected Denormalizer(IDialect db, Func<IDbConnection> connectionFactory)
        {
            _connectionFactory = connectionFactory;
            Db = db;
        }

        protected abstract Expression<Func<TEntity, object>> GetId();

        protected Expression<Func<TEntity, object>> GetId(Expression<Func<TEntity, object>> expression)
        {
            return expression;
        }

        protected void Insert<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            var cmd = Db.Insert<TEntity>()
                .MapFrom(publishedEvent.Payload);
            ExecuteSingle(cmd);
        }

        protected void Upsert<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            var cmd = Db.Upsert<TEntity>()
                .MapFrom(publishedEvent.Payload)
                .WithId(GetId());
            ExecuteSingle(cmd);
        }

        protected void Update<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            var cmd = Db.Update<TEntity>()
                .MapFrom(publishedEvent.Payload)
                .WithId(GetId());
            ExecuteSingle(cmd);
        }

        protected void DeleteSingle(Expression<Func<TEntity, bool>> predicate)
        {
            var cmd = Db.Delete<TEntity>()
                .Where(predicate)
                .ToCommand();
            ExecuteSingle(cmd);
        }

        protected virtual void ExecuteSingle(IDbCommand command)
        {
            Execute(command, 1);
        }

        protected virtual void Execute(IDbCommand command, long expectedRowsAffected)
        {
            Execute(command, rowsAffected => rowsAffected == expectedRowsAffected);
        }

        protected virtual IDbConnection GetReadModelConnection()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["ReadModel"]
                .ConnectionString;
            var conn = _connectionFactory();
            conn.ConnectionString = connectionString;
            return conn;
        }

        protected virtual void Execute(IDbCommand command, Func<long, bool> correctNumberOfRowsUpdated)
        {
            if (command == null)
                throw new NullReferenceException();
            if (correctNumberOfRowsUpdated == null)
                throw new NullReferenceException();

            using (var conn = GetReadModelConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    command.Connection = conn;
                    command.Transaction = tx;
                    var rowsAffected = command.ExecuteNonQuery();
                    if (!correctNumberOfRowsUpdated(rowsAffected))
                    {
                        tx.Rollback();
                        throw new UnexpectedNumberOfRowsAffectedException(command, rowsAffected);
                    }
                    tx.Commit();
                }
                conn.Close();
            }
        }




    }

}
