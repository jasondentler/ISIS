using System;

namespace ISIS.Schedule
{

    public class when_serializing_TopicCodeDescriptionChangedEvent
        : JsonEventSerializationFixture<TopicCodeDescriptionChangedEvent>
    {

        protected override TopicCodeDescriptionChangedEvent GivenEvent()
        {
            return new TopicCodeDescriptionChangedEvent(
                Guid.NewGuid(), "New Description", "Previous Description");
        }

    }
}
