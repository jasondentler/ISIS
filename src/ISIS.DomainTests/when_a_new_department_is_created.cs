using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests
{
    [TestFixture]
    public class when_a_new_department_is_created : DepartmentFixture
    {

        protected override IEnumerable<SourcedEvent> Given()
        {
            // No previous history
            ExpectedEventCount = 1;
            return base.Given();
        }

        protected override void When()
        {
            AggregateRoot = new Department(Name);
        }

        [Test]
        public void then_it_should_create_a_new_DepartmentCreatedEvent()
        {
            var firstEvent = PublishedEvents.First() as DepartmentCreatedEvent;
            Assert.That(firstEvent, Is.Not.Null);
            Assert.That(firstEvent.Name, Is.EqualTo(Name));
        }

        [Test]
        public void it_should_do_nothing_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(1));
        }

    }
}
