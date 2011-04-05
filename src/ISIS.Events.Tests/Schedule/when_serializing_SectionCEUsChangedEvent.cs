using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCEUsChangedEvent
        : JsonEventSerializationFixture<SectionCEUsChangedEvent>
    {

        protected override SectionCEUsChangedEvent GivenEvent()
        {
            return new SectionCEUsChangedEvent(
                Guid.NewGuid(),
                0.7M);
        }

    }
}
