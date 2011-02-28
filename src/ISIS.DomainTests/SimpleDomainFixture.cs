using Ncqrs.Commanding;
using Ncqrs.Spec;

namespace ISIS.DomainTests
{
    public abstract class SimpleDomainFixture<TCommand, TEvent>
        : OneEventTestFixture<TCommand, TEvent>
        where TCommand : ICommand
    {

        protected override void Given()
        {
            Configuration.Configure();
            base.Given();
        }

    }
}
