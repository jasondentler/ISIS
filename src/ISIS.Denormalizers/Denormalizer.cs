using System;
using System.Data;
using System.Linq.Expressions;
using AutoMapper;
using FluentDML.Dialect;
using log4net;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{

    public abstract class Denormalizer<TEntity> : IDenormalizer
        where TEntity : IEntity
    {

        protected static readonly ILog Log = LogManager.GetLogger("ISIS.Denormalizer`1[[" + typeof (TEntity) + "]]");

        protected IDialect Db { get; private set; }

        protected Denormalizer(IDialect db)
        {
            Db = db;
        }

        protected IMappingExpression<TEvent, TEntity> CreateMap<TEvent>()
        {
            return Mapper.CreateMap<TEvent, TEntity>();
        }

        protected abstract Expression<Func<TEntity, object>> GetId();

        protected Expression<Func<TEntity, object>> GetId(Expression<Func<TEntity, object>> expression)
        {
            return expression;
        }

        protected void Insert<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            Log.DebugFormat("Inserting into {0} from {1}", typeof(TEntity), typeof(TEvent));
            var cmd = Db.Insert<TEntity>()
                .MapFrom(publishedEvent.Payload);
            ExecuteSingle(cmd);
        }

        protected void Upsert<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            Log.DebugFormat("Inserting into {0} from {1}", typeof(TEntity), typeof(TEvent));
            var cmd = Db.Upsert<TEntity>()
                .MapFrom(publishedEvent.Payload)
                .WithId(GetId());
            Execute(cmd, rowsAffect => rowsAffect > 0 && rowsAffect <= 2);
        }

        protected void Update<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            Log.DebugFormat("Updating {0} from {1}", typeof(TEntity), typeof(TEvent));
            var cmd = Db.Update<TEntity>()
                .MapFrom(publishedEvent.Payload)
                .WithId(GetId());
            ExecuteSingle(cmd);
        }

        protected void DeleteSingle(Expression<Func<TEntity, bool>> predicate)
        {
            Log.DebugFormat("Deleting {0}", typeof(TEntity));
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
            return Db.GetConnection("ReadModel");
        }

        protected virtual void Execute(IDbCommand command, Func<long, bool> correctNumberOfRowsAffected)
        {
            if (command == null)
                throw new NullReferenceException();
            if (correctNumberOfRowsAffected == null)
                throw new NullReferenceException();

            if (Log.IsDebugEnabled)
            {
                Log.Debug(command.CommandText);
                foreach (IDbDataParameter param in command.Parameters)
                {
                    Log.DebugFormat("{0} = {1}", param.ParameterName, param.Value);
                }
            }

            using (var conn = GetReadModelConnection())
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    command.Connection = conn;
                    command.Transaction = tx;
                    var rowsAffected = command.ExecuteNonQuery();
                    if (!correctNumberOfRowsAffected(rowsAffected))
                    {
                        Log.Debug("Incorrect number of rows affected. Rolling back transaction");
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
