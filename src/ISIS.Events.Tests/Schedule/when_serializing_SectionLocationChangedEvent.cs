using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionLocationChangedEvent
        : JsonEventSerializationFixture<SectionLocationChangedEvent>
    {

        protected override SectionLocationChangedEvent GivenEvent()
        {
            return new SectionLocationChangedEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "ACC",
                "Main Campus");
        }

    }
}
