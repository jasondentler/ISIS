using NHibernate;

namespace ISIS.NHibernateReadModel
{
    public class RepositoryFactory : IRepositoryFactory
    {

        private readonly ISessionFactory _sessionFactory;

        public RepositoryFactory(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public IRepository CreateRepository()
        {
            return new Repository(_sessionFactory.OpenSession());
        }
    }
}
