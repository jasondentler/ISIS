using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionMadePendingEvent
        : JsonEventSerializationFixture<SectionMadePendingEvent>
    {

        protected override SectionMadePendingEvent GivenEvent()
        {
            return new SectionMadePendingEvent(
                Guid.NewGuid());
        }

    }
}
