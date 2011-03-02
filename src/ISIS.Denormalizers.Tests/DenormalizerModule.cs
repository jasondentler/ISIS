using FluentDML.Dialect;
using FluentDML.Mapping;
using FluentDML.NHibernateAdapter;
using NHibernate;
using NHibernate.Cfg;
using Ninject;
using Ninject.Modules;

namespace ISIS
{
    public class DenormalizerModule : NinjectModule 
    {
        public override void Load()
        {

            Kernel.Bind<Configuration>()
                .ToConstant(NHibernateReadModel.ReadModelConfiguration.Configuration);

            Kernel.Bind<ISessionFactory>()
                .ToConstant(NHibernateReadModel.ReadModelConfiguration.SessionFactory);

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
