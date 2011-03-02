using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_the_same_CIP_is_assigned : 
        DomainFixture<ChangeCIPCommand>
    {

        private const string CIP = "123456";

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseCIPChangedEvent(EventSourceId, CIP);
        }

        protected override ChangeCIPCommand WhenExecuting()
        {
            return new ChangeCIPCommand()
            {
                CourseId = EventSourceId,
                CIP = CIP
            };
        }

        [Then]
        public void it_should_do_nothing()
        {
            Assert.That(PublishedEvents, Is.Empty);
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

    }
}
