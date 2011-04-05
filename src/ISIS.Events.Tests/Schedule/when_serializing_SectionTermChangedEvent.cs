using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionTermChangedEvent
        : JsonEventSerializationFixture<SectionTermChangedEvent>
    {

        protected override SectionTermChangedEvent GivenEvent()
        {
            return new SectionTermChangedEvent(
                Guid.NewGuid(), Guid.NewGuid(), "211FA", "Fall 2011");
        }

    }
}
