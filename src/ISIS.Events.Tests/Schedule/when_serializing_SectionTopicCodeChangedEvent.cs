using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionTopicCodeChangedEvent
        : JsonEventSerializationFixture<SectionTopicCodeChangedEvent>
    {

        protected override SectionTopicCodeChangedEvent GivenEvent()
        {
            return new SectionTopicCodeChangedEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "A",
                "Academic TDC Course Code");
        }

    }
}
