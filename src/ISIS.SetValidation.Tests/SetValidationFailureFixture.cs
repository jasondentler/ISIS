using Ncqrs.Commanding;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS
{
    public abstract class SetValidationFailureFixture<TCommand, TEntity>
        : SetValidationTestFixture<TCommand, TEntity>
        where TCommand : ICommand
    {

        [Then]
        public void it_should_throw_SetValidationException()
        {
            Assert.That(CaughtException, Is.InstanceOf<SetValidationException>());
        }
        
    }

}
