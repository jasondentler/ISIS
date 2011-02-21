using ISIS.NHibernateReadModel;
using Ninject.Modules;
using NHibernate;
using Ninject;

namespace ISIS.Web
{
    public class NHibernateModule : NinjectModule 
    {

        public override void Load()
        {
            Kernel.Bind<ISessionFactory>()
                .ToConstant(ReadModelConfiguration.SessionFactory);

            Kernel.Bind<ISession>()
                .ToMethod(ctx => Kernel.Get<ISessionFactory>().OpenSession())
                .InRequestScope();

            Kernel.Bind<IReadRepository>()
                .To<ReadRepository>()
                .InRequestScope();

        }

    }
}