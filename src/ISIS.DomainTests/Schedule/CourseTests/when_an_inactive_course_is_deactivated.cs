using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_an_inactive_course_is_deactivated : 
        DomainFixture<DeactivateCourseCommand>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "1304");
            yield return new CourseDeactivatedEvent(EventSourceId);
        }

        protected override DeactivateCourseCommand WhenExecuting()
        {
            return new DeactivateCourseCommand() { CourseId = EventSourceId };
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
