using System.Collections.Generic;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_removing_a_nonexistent_course_type : 
        DomainFixture<RemoveCourseTypeFromCourse>
    {

        private const CourseTypes Type = CourseTypes.ACAD;

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseTypeAddedToCourseEvent(EventSourceId, CourseTypes.WECM, new[] {CourseTypes.WECM});
        }

        protected override RemoveCourseTypeFromCourse WhenExecuting()
        {
            return new RemoveCourseTypeFromCourse()
                       {
                           CourseId = EventSourceId,
                           Type = Type
                       };
        }

        [Then]
        public void then_it_should_do_nothing()
        {
            Assert.That(PublishedEvents, Is.Empty);
        }

        [Then]
        public void then_it_should_not_throw()
        {
            Assert.That(CaughtException, Is.Null);
        }


    }
}
