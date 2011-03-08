using System.Collections.Generic;
using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_removing_a_course_type : 
        SimpleDomainFixture<RemoveCourseTypeFromCourse, CourseTypeRemovedFromCourseEvent>
    {

        private const CourseTypes Type = CourseTypes.ACAD;

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseTypeAddedToCourseEvent(EventSourceId, CourseTypes.ACAD, new[] { CourseTypes.ACAD });
            yield return new CourseTypeAddedToCourseEvent(EventSourceId, CourseTypes.NF, new[] {CourseTypes.NF});
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
        public void then_it_should_create_a_new_CourseTypeRemovedFromCourseEvent()
        {
            Assert.That(TheEvent.CourseId, Is.EqualTo(EventSourceId));
            Assert.That(TheEvent.TypeRemoved, Is.EqualTo(Type));
            Assert.That(TheEvent.CurrentTypes.Count(), Is.EqualTo(1));
            Assert.That(TheEvent.CurrentTypes, Contains.Item(CourseTypes.NF));
        }


    }
}
