using NHibernate;

namespace ISIS.NHibernateReadModel
{
    public class RepositoryFactory : IRepositoryFactory
    {

        private readonly ISessionFactory _sessionFactory;

        public RepositoryFactory()
        {
            _sessionFactory = ReadModelConfiguration.SessionFactory;
        }

        public IRepository CreateRepository()
        {
            return new Repository(_sessionFactory.OpenSession());
        }
    }
}
