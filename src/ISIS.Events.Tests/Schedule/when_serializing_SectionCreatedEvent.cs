using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCreatedEvent
        : JsonEventSerializationFixture<SectionCreatedEvent>
    {

        protected override SectionCreatedEvent GivenEvent()
        {
            return new SectionCreatedEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "BIOL",
                "1301",
                Guid.NewGuid(),
                "211FA",
                "Fall 2011",
                "01");
        }

    }
}
