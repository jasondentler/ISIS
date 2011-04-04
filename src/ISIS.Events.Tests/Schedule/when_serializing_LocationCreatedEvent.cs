using System;

namespace ISIS.Schedule
{

    public class when_serializing_LocationCreatedEvent
        : JsonEventSerializationFixture<LocationCreatedEvent>
    {

        protected override LocationCreatedEvent GivenEvent()
        {
            return new LocationCreatedEvent(
                Guid.NewGuid(), "ACC", "Main Campus");
        }

    }
}
