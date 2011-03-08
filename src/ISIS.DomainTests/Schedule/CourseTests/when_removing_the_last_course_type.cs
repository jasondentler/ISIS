using System.Collections.Generic;
using Ncqrs.Spec;

namespace ISIS.Schedule.CourseTests
{
    [Specification]
    public class when_removing_the_last_course_type : 
        ExceptionTestFixture<RemoveCourseTypeFromCourse, InvalidStateException>
    {

        private const CourseTypes Type = CourseTypes.ACAD;

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new CreditCourseCreatedEvent(EventSourceId, "BIOL", "2302");
            yield return new CourseTypeAddedToCourseEvent(EventSourceId, Type, new[] { Type });
        }

        protected override RemoveCourseTypeFromCourse WhenExecuting()
        {
            return new RemoveCourseTypeFromCourse()
                       {
                           CourseId = EventSourceId,
                           Type = Type
                       };
        }
        

    }
}
