using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Config;
using Ncqrs.Domain.Storage;
using Ninject;

namespace ISIS
{
    public class Configuration : IEnvironmentConfiguration 
    {

        public static void Configure()
        {
            if (NcqrsEnvironment.IsConfigured) return;
            var cfg = new Configuration();
            NcqrsEnvironment.Configure(cfg);
        }

        private readonly ICommandService _commandService;
        private readonly IAggregateRootCreationStrategy _aggregateRootCreationStrategy;
        private readonly IDomainRepository _domainRepository;

        private Configuration()
        {
            _commandService = BuildCommandService();
            _aggregateRootCreationStrategy = BuildAggregateRootCreationStrategy();
            _domainRepository = BuildDomainRepository(_aggregateRootCreationStrategy);
        }

        private static ICommandService BuildCommandService()
        {
            var commandService = new CommandService();
            commandService.Configure();
            commandService.AddInterceptor(new ValidationCommandInterceptor());
            return commandService;
        }

        private static IAggregateRootCreationStrategy BuildAggregateRootCreationStrategy()
        {
            var kernel = new StandardKernel();
            var creationStrategy = new NinjectAggregateRootCreationStrategy(kernel);
            return creationStrategy;
        }

        private static IDomainRepository BuildDomainRepository(IAggregateRootCreationStrategy creationStrategy)
        {
            return new DomainRepository(creationStrategy);
        }

        public bool TryGet<T>(out T result) where T : class
        {
            result = null;
            if (typeof(T) == typeof(ICommandService))
                result = (T) _commandService;
            if (typeof(T) == typeof(IAggregateRootCreationStrategy))
                result = (T) _aggregateRootCreationStrategy;
            if (typeof(T) == typeof(IDomainRepository))
                result = (T) _domainRepository;
            return result != null;
        }

    }
}
