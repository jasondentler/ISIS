using System;

namespace ISIS.Schedule
{

    public class when_serializing_ContinuingEducationCourseCreatedEvent
        : JsonEventSerializationFixture<ContinuingEducationCourseCreatedEvent>
    {

        protected override ContinuingEducationCourseCreatedEvent GivenEvent()
        {
            return new ContinuingEducationCourseCreatedEvent(
                Guid.NewGuid(), "ARTS", "1001");
        }

    }
}
