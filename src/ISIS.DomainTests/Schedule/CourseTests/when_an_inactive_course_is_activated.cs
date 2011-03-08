using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_an_inactive_course_is_activated : 
        SimpleDomainFixture<ActivateCourseCommand, CourseActivatedEvent>
    {

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "1304");
            yield return new CourseDeactivatedEvent(EventSourceId);
        }

        protected override ActivateCourseCommand WhenExecuting()
        {
            return new ActivateCourseCommand() {CourseId = EventSourceId};
        }

        [Then]
        public void it_should_activate_the_course()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
        }

    }
}
