using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Config.Ninject;
using Ncqrs.Domain.Storage;
using Ninject;
using Ninject.Modules;

namespace ISIS
{
    public class NcqrsModule : NinjectModule 
    {

        public static NinjectSettings GetSettings()
        {
            return new NinjectSettings()
                       {
                           InjectNonPublic = true,
                           InjectAttribute = typeof (ISIS.InjectAttribute)
                       };
        }


        public override void Load()
        {

            Kernel.Bind<IKernel>().ToConstant(Kernel);

            var commandService = new CommandService();
            commandService.Configure();
            commandService.AddInterceptor(new ValidationCommandInterceptor());
            Kernel.Bind<ICommandService>().ToConstant(commandService);

            Kernel.Bind<IAggregateRootCreationStrategy>().To<NinjectAggregateRootCreationStrategy>();

            Kernel.Bind<IClock>().To<DateTimeBasedClock>();

        }
    }
}
