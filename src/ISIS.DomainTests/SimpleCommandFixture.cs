using System.Linq;
using Ncqrs.Commanding;
using Ncqrs.Domain;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace ISIS.DomainTests
{
    public abstract class SimpleCommandFixture<TCommand, TAggregateRoot, TEvent>
        : CommandFixture<TCommand, TAggregateRoot>
        where TCommand : ICommand
        where TAggregateRoot : AggregateRoot
        where TEvent : ISourcedEvent

    {

        protected TEvent TheEvent
        {
            get { return (TEvent) PublishedEvents.Single(); }
        }

        [Test]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

        [Test]
        public void it_should_do_nothing_more()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(1));
        }

        [Test]
        public void it_should_publish_correct_event_type()
        {
            Assert.That(PublishedEvents.Single(), Is.InstanceOf<TEvent>());
        }

    }
}
