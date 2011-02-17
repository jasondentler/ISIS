using System.Configuration;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.SQL;
using Ninject.Modules;
using Ninject;


namespace ISIS.Web
{
    public class NcqrsModule
        : NinjectModule 
    {
        public override void Load()
        {
            var eventStoreConnectionString = ConfigurationManager.ConnectionStrings["EventStore"].ConnectionString;
            Kernel.Bind<IEventStore>()
                .ToMethod(ctx => new MsSqlServerEventStore(eventStoreConnectionString))
                .InSingletonScope();

            Kernel.Bind<IEventBus>()
                .ToMethod(ctx => new InProcessEventBus().RegisterDenormalizers(Kernel))
                .InSingletonScope();

            Kernel.Bind<IAggregateRootCreationStrategy>()
                .To<SimpleAggregateRootCreationStrategy>()
                .InSingletonScope();

            Kernel.Bind<IDomainRepository>()
                .ToMethod(ctx =>
                          new DomainRepository(Kernel.Get<IEventStore>(),
                                               Kernel.Get<IEventBus>(),
                                               null,
                                               Kernel.Get<IAggregateRootCreationStrategy>()))
                .InSingletonScope();

            Kernel.Bind<ICommandService>()
                .ToMethod(ctx =>
                              {
                                  var cs = new CommandService();
                                  cs.Configure();
                                  return cs;
                              })
                .InSingletonScope();

        }
    }
}