using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_an_active_course_is_activated : 
        DomainFixture<ActivateCourseCommand>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CourseCreatedEvent(EventSourceId, "BIOL", "1304");
            yield return new CourseActivatedEvent(EventSourceId);
        }

        protected override ActivateCourseCommand WhenExecuting()
        {
            return new ActivateCourseCommand() {CourseId = EventSourceId};
        }

        [Then]
        public void it_should_do_nothing()
        {
            Assert.That(PublishedEvents.Count(), Is.EqualTo(0));
        }

        [Then]
        public void it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }

    }
}
