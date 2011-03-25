using System;

namespace ISIS.Schedule
{

    public class when_serializing_TopicCodeAbbreviationChangedEvent
        : JsonEventSerializationFixture<TopicCodeAbbreviationChangedEvent>
    {

        protected override TopicCodeAbbreviationChangedEvent GivenEvent()
        {
            return new TopicCodeAbbreviationChangedEvent(Guid.NewGuid(), "BANK", "MONEY");
        }

    }
}
