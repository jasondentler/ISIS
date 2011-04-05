using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCIPChangedEvent
        : JsonEventSerializationFixture<SectionCIPChangedEvent>
    {

        protected override SectionCIPChangedEvent GivenEvent()
        {
            return new SectionCIPChangedEvent(
                Guid.NewGuid(),
                "123456");
        }

    }
}
