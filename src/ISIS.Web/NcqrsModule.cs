using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Storage;
using Ncqrs.Eventing.Storage.SQL;
using Ninject.Modules;
using Ninject;
using Ncqrs;


namespace ISIS.Web
{
    public class NcqrsModule
        : NinjectModule 
    {
        public override void Load()
        {

            Kernel.Bind<IEventStore>()
                .To<MsSqlServerEventStore>()
                .InSingletonScope();

            Kernel.Bind<IEventBus>()
                .ToMethod(ctx => new InProcessEventBus().RegisterDenormalizers())
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
                .ToMethod(ctx => new CommandService().Configure())
                .InSingletonScope();

        }
    }
}