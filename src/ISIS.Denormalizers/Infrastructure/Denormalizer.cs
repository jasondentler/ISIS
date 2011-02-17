using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using WebMatrix.Data;

namespace ISIS.Infrastructure
{
    public abstract class Denormalizer<TEntity, TPrimaryKey> : IDenormalizer
        where TEntity : Entity, new()
    {

        private static readonly ILog Log =
            Ncqrs.LogManager.GetLogger(typeof (Denormalizer<TEntity, TPrimaryKey>));

        protected Denormalizer()
        {
            Log.DebugFormat("Creating denormalizer {0}", GetType());
        }

        protected abstract IEnumerable<ColumnInformation> GetDataColumns();

        public virtual void Setup()
        {
            var db = Database.Open(Entity.ConnectionStringName);
            var e = new TEntity();
            var tableName = e.TableName;
            var primaryKeyField = e.PrimaryKeyField;
            var primaryKey = new ColumnInformation<TPrimaryKey>(primaryKeyField);
            var columns = new[] {primaryKey}.Union(GetDataColumns());
            var columnSQL = string.Join(",",
                                        columns.Select(c => c.ToString()).ToArray());

            db.Execute(string.Format("CREATE TABLE {0} ({1})", tableName, columnSQL));
        }

        public virtual void TryTeardown()
        {
            try
            {
                var e = new TEntity();
                var tableName = e.TableName;
                var db = Database.Open(Entity.ConnectionStringName);
                db.Execute(string.Format("DROP TABLE {0}", tableName));
            }
            catch(Exception exception)
            {
                Log.Warn(exception);
            }
        }


    }
}
