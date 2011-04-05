using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionDatesChangedEvent
        : JsonEventSerializationFixture<SectionDatesChangedEvent>
    {

        protected override SectionDatesChangedEvent GivenEvent()
        {
            return new SectionDatesChangedEvent(
                Guid.NewGuid(), DateTime.Today, DateTime.Today.AddDays(1));
        }

    }
}
