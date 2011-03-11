using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseTypeRemovedFromCourseEvent
        : JsonEventSerializationFixture<CourseTypeRemovedFromCourseEvent>
    {

        protected override CourseTypeRemovedFromCourseEvent GivenEvent()
        {
            return new CourseTypeRemovedFromCourseEvent(Guid.NewGuid(),
                                                        CourseTypes.ACAD, new CourseTypes[0]);
        }

    }
}
