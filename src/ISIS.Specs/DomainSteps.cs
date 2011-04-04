using FluentValidation;
using Ncqrs;
using Ncqrs.Config.Ninject;
using Newtonsoft.Json;
using Ninject;
using NUnit.Framework;
using TechTalk.SpecFlow;
using System.Linq;


namespace ISIS
{
    [Binding]
    public class DomainSteps
    {

        [BeforeScenario("domain")]
        public void Setup()
        {
            if (NcqrsEnvironment.IsConfigured) return;
            var kernel = new StandardKernel(NcqrsModule.GetSettings(), new NcqrsModule());
            NcqrsEnvironment.Configure(new NinjectConfiguration(kernel));
        }

        [Then(@"it should do nothing")]
        public void ThenItShouldDoNothing()
        {
            Assert.That(DomainHelper.GetEvents(), Is.Empty);
        }
        
        [Then(@"it should do nothing else")]
        public void ThenItShouldDoNothingElse()
        {
            var untestedEvents = DomainHelper.GetUntestedEvents();

            var data = untestedEvents.Select(e =>
                                             string.Format("{0}: {1}",
                                                           e.GetType().ToString(),
                                                           JsonConvert.SerializeObject(e)));

            var dataString = string.Join(",\r\n", data);

            var msg = string.Format("The following events weren't checked by the scenario: \r\n{0}",
                                    dataString);

            Assert.That(untestedEvents, Is.Empty, msg);
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
