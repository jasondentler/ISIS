using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.DomainTests.TermTests
{
    [TestFixture]
    public class when_a_new_term_is_created : 
        OneEventTestFixture<CreateTermCommand, TermCreatedEvent>
    {

        private const string TermName = "Spring 2010";

        protected override IEnumerable<object> GivenEvents()
        {
            return new object[0];
        }

        protected override CreateTermCommand WhenExecuting()
        {
            return new CreateTermCommand()
            {
                Name = TermName
            };
        }

        [Test]
        public void then_it_should_create_a_new_TermCreatedEvent()
        {
            Assert.That(TheEvent.Name, Is.EqualTo(TermName));
        }


    }
}
