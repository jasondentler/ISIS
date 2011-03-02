using System;
using System.Linq.Expressions;
using FluentDML;
using FluentDML.Dialect;
using log4net;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{

    public abstract class Denormalizer<TEntity> : FluentDML.ReadModel.Denormalizer<TEntity>
        where TEntity : IEntity
    {

        protected static readonly ILog Log = LogManager.GetLogger("ISIS.Denormalizer`1[[" + typeof (TEntity) + "]]");

        protected Denormalizer(IDialect dialect)
            : base(dialect, "ReadModel")
        {
        }

        protected Expression<Func<TEntity, object>> GetId(Expression<Func<TEntity, object>> expression)
        {
            return expression;
        }

        protected void Insert<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            Log.DebugFormat("Inserting into {0} from {1}", typeof(TEntity), typeof(TEvent));
            Insert(publishedEvent.Payload);
        }

        protected void Upsert<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            Log.DebugFormat("Upserting into {0} from {1}", typeof(TEntity), typeof(TEvent));
            Upsert(publishedEvent.Payload);
        }

        protected void Update<TEvent>(IPublishedEvent<TEvent> publishedEvent)
        {
            Log.DebugFormat("Updating {0} from {1}", typeof(TEntity), typeof(TEvent));
            Update(publishedEvent.Payload);
        }
        
    }

}
