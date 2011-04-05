using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCourseTypeRemovedEvent
        : JsonEventSerializationFixture<SectionCourseTypeRemovedEvent>
    {

        protected override SectionCourseTypeRemovedEvent GivenEvent()
        {
            return new SectionCourseTypeRemovedEvent(
                Guid.NewGuid(),
                CourseTypes.ACAD,
                new[] {CourseTypes.ACAD, CourseTypes.CWECM});
        }

    }
}
