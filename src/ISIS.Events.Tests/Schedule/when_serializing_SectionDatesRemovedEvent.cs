using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionDatesRemovedEvent
        : JsonEventSerializationFixture<SectionDatesRemovedEvent>
    {

        protected override SectionDatesRemovedEvent GivenEvent()
        {
            return new SectionDatesRemovedEvent(
                Guid.NewGuid());
        }

    }
}
