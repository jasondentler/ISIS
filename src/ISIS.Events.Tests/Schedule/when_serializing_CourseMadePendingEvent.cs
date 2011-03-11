using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseMadePendingEvent
        : JsonEventSerializationFixture<CourseMadePendingEvent>
    {

        protected override CourseMadePendingEvent GivenEvent()
        {
            return new CourseMadePendingEvent(Guid.NewGuid());
        }

    }
}
