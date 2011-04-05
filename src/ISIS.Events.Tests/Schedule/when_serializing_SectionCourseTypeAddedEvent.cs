using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCourseTypeAddedEvent
        : JsonEventSerializationFixture<SectionCourseTypeAddedEvent>
    {

        protected override SectionCourseTypeAddedEvent GivenEvent()
        {
            return new SectionCourseTypeAddedEvent(
                Guid.NewGuid(),
                CourseTypes.ACAD,
                new[] {CourseTypes.ACAD, CourseTypes.R50});
        }

    }
}
