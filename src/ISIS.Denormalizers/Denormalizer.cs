namespace ISIS
{

    public abstract class Denormalizer
    {
        private readonly IRepositoryFactory _repositoryFactory;

        protected Denormalizer(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        protected IRepository OpenRepository()
        {
            return _repositoryFactory.CreateRepository();
        }
    }

}
