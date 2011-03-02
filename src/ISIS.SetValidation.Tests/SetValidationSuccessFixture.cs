using Ncqrs.Commanding;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS
{
    public abstract class SetValidationSuccessFixture<TCommand, TEntity>
        : SetValidationTestFixture<TCommand, TEntity>
        where TCommand : ICommand
    {

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }
        
    }

}
