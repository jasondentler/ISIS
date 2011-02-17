using NHibernate.Tool.hbm2ddl;

namespace ISIS.Denormalizers.Tests
{
    public class DatabaseHelper
    {

        public void Setup()
        {
            var cfg = NHibernateReadModel.NHibernateModule.Configuration;
            TearDown();

            var export = new SchemaExport(cfg);
            export.Create(false, true);
        }

        public void TearDown()
        {
            DoNotCloseConnectionProvider.CloseDatabase();
        }

    }
}
