using FluentValidation;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS
{
    [Binding]
    public class DomainSteps
    {
        [Then(@"it should do nothing")]
        public void ThenItShouldDoNothing()
        {
            Assert.That(DomainHelper.GetEvents(), Is.Empty);
        }
        
        [Then(@"it should do nothing else")]
        public void ThenItShouldDoNothingElse()
        {
            Assert.That(DomainHelper.AllEventsWereTested(), Is.True);
        }

        [Then(@"the command is invalid")]
        public void ThenTheCommandIsInvalid()
        {
            var ex = DomainHelper.GetException<ValidationException>();
            Assert.That(ex, Is.Not.Null);
        }

        [Then(@"the aggregate state is invalid")]
        public void ThenTheAggregateStateIsInvalid()
        {
            var ex = DomainHelper.GetException<InvalidStateException>();
            Assert.That(ex, Is.Not.Null);
        }

        [Then(@"the error is ""(.*)""")]
        public void ThenTheErrorIs(string message)
        {
            var ex = DomainHelper.GetException();
            var actual = ex.Message;
            if (ex is ValidationException)
                actual = actual.Replace("Validation failed: \r\n -- ", string.Empty);
            Assert.That(actual, Is.EqualTo(message));
        }



    }
}
