using NHibernate.Tool.hbm2ddl;

namespace ISIS.Denormalizers.Tests
{
    public class DatabaseHelper
    {

        public void Setup()
        {
            var cfg = NHibernateReadModel.NHibernateModule.Configuration;
            var sessionFactory = NHibernateReadModel.NHibernateModule.SessionFactory;
            TearDown();

            var export = new SchemaExport(cfg);
            export.Create(false, true);
        }

        public void TearDown()
        {
            var cfg = NHibernateReadModel.NHibernateModule.Configuration;
            var sessionFactory = NHibernateReadModel.NHibernateModule.SessionFactory;
            var export = new SchemaExport(cfg);
            export.Drop(false, true);
        }

    }
}
