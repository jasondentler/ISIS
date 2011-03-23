using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Config;

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

        private Configuration()
        {
            _commandService = BuildCommandService();
        }

        private static ICommandService BuildCommandService()
        {
            var commandService = new CommandService();
            commandService.Configure();
            commandService.AddInterceptor(new ValidationCommandInterceptor());
            return commandService;
        }

        public bool TryGet<T>(out T result) where T : class
        {
            result = null;
            if (typeof(T) == typeof(ICommandService))
                result = (T) _commandService;
            return result != null;
        }

    }
}
