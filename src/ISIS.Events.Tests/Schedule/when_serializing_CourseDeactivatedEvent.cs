using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseDeactivatedEvent
        : JsonEventSerializationFixture<CourseDeactivatedEvent>
    {

        protected override CourseDeactivatedEvent GivenEvent()
        {
            return new CourseDeactivatedEvent(Guid.NewGuid());
        }

    }
}
