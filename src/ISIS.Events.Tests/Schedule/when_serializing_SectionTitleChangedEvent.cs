using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionTitleChangedEvent
        : JsonEventSerializationFixture<SectionTitleChangedEvent>
    {

        protected override SectionTitleChangedEvent GivenEvent()
        {
            return new SectionTitleChangedEvent(
                Guid.NewGuid(), "previous title", "new title");
        }

    }
}
