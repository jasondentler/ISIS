using Ncqrs.Commanding;
using Ncqrs.Spec;

namespace ISIS.DomainTests
{
    public abstract class DomainFixture<TCommand>
         : BigBangTestFixture<TCommand> 
        where TCommand : ICommand
    {

        protected override void Given()
        {
            Configuration.Configure();
            base.Given();
        }

    }
}
