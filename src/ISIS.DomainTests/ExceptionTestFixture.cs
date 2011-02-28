using System;
using Ncqrs.Commanding;

namespace ISIS.DomainTests
{
    public abstract class ExceptionTestFixture<TCommand, TException>
        : Ncqrs.Spec.ExceptionTestFixture<TCommand, TException>
        where TCommand : ICommand
        where TException : Exception
    {

        protected override void Given()
        {
            Configuration.Configure();
            base.Given();
        }

    }
}
