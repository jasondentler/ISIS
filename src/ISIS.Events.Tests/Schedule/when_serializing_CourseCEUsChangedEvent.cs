using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseCEUsChangedEvent
        : JsonEventSerializationFixture<CourseCEUsChangedEvent>
    {

        protected override CourseCEUsChangedEvent GivenEvent()
        {
            return new CourseCEUsChangedEvent(Guid.NewGuid(), 0.7M);
        }

    }
}
