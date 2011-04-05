using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionNumberChangedEvent
        : JsonEventSerializationFixture<SectionNumberChangedEvent>
    {

        protected override SectionNumberChangedEvent GivenEvent()
        {
            return new SectionNumberChangedEvent(
                Guid.NewGuid(), "01");
        }

    }
}
