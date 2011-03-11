using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseTypeAddedToCourseEvent
        : JsonEventSerializationFixture<CourseTypeAddedToCourseEvent>
    {

        protected override CourseTypeAddedToCourseEvent GivenEvent()
        {
            return new CourseTypeAddedToCourseEvent(Guid.NewGuid(),
                                                    CourseTypes.ACAD,
                                                    new CourseTypes[0]);
        }

    }
}
