using System;

namespace ISIS.Schedule
{

    public class when_serializing_TopicCodeCreatedEvent
        : JsonEventSerializationFixture<TopicCodeCreatedEvent>
    {

        protected override TopicCodeCreatedEvent GivenEvent()
        {
            return new TopicCodeCreatedEvent(Guid.NewGuid(),"BANK","Banking/Finance");
        }

    }
}
