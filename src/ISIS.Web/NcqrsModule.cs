using System.Configuration;
using FluentDML.Dialect;
using FluentDML.Mapping;
using FluentDML.NHibernateAdapter;
using ISIS.Validation;
using Ncqrs;
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

            Kernel.Bind<IUniqueIdentifierGenerator>()
                .To<GuidCombGenerator>();

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
                          new DomainRepository(Kernel.Get<IAggregateRootCreationStrategy>()))
                .InSingletonScope();

            Kernel.Bind<ICommandServiceInterceptor>()
                .To<ValidationCommandInterceptor>()
                .InSingletonScope();

            Kernel.Bind<ICommandService>()
                .ToMethod(ctx =>
                              {
                                  var cs = new CommandService();
                                  cs.Configure();
                                  var interceptors = ctx.Kernel.GetAll<ICommandServiceInterceptor>();
                                  foreach (var interceptor in interceptors)
                                      cs.AddInterceptor(interceptor);
                                  return cs;
                              })
                .InSingletonScope();

            Kernel.Bind<IMapMaker>()
                .ToMethod(ctx => ctx.Kernel.Get<NHibernateMapMaker>());

            Kernel.Bind<Map>()
                .ToMethod(ctx => ctx.Kernel.Get<IMapMaker>().MakeMap())
                .InSingletonScope();

            Kernel.Bind<IDialect>()
                .ToMethod(ctx => new MsSqlDialect(ctx.Kernel.Get<Map>()))
                .InSingletonScope();


        }
    }
}