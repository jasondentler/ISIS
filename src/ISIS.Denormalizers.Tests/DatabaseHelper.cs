using NHibernate.Tool.hbm2ddl;

namespace ISIS
{
    public class DatabaseHelper
    {

        public void Setup()
        {
            var cfg = NHibernateReadModel.ReadModelConfiguration.Configuration;
            var export = new SchemaExport(cfg);
            export.Drop(false, true);
            export.Create(false, true);
        }

        public void TearDown()
        {
        }

    }
}
