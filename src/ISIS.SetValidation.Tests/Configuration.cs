using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Spec.Fakes;

namespace ISIS
{
    public class Configuration : EnvironmentConfigurationWrapper
    {

        public Configuration(SetValidationCommandInterceptor interceptor)
        {
            var commandService = new CommandService();
            commandService.Configure();
            commandService.AddInterceptor(interceptor);
            Register<ICommandService>(commandService);
        }

    }
}
