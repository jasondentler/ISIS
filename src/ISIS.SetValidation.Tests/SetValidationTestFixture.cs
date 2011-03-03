using System.Collections.Generic;
using Ncqrs;
using Ncqrs.Commanding;
using Ncqrs.Spec;
using Ninject;

namespace ISIS
{
    public abstract class SetValidationTestFixture<TCommand, TEntity>
        : BigBangTestFixture<TCommand>
        where TCommand : ICommand
    {

        protected abstract IEnumerable<TEntity> GivenQueryResults();

        protected override void Given()
        {
            var kernel = new StandardKernel(new SetValidatorModule());
            var repo = new ReadRepositoryFake(GivenQueryResults());
            kernel.Bind<IReadRepository>().ToConstant(repo);
            var interceptor = new SetValidationCommandInterceptor(kernel);
            var cfg = new Configuration(interceptor);
            cfg.Push();
            base.Given();
        }

        protected override void Finally()
        {
            base.Finally();
            var cfg = (Configuration)NcqrsEnvironment.CurrentConfiguration;
            cfg.Pop();
        }

        protected override IEnumerable<object> GivenEvents()
        {
            return new object[0];
        }
        
    }

}
