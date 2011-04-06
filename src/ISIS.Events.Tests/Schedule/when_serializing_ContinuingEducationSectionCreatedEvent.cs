using System;

namespace ISIS.Schedule
{

    public class when_serializing_ContinuingEducationSectionCreatedEvent
        : JsonEventSerializationFixture<ContinuingEducationSectionCreatedEvent>
    {

        protected override ContinuingEducationSectionCreatedEvent GivenEvent()
        {
            return new ContinuingEducationSectionCreatedEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "ARTS",
                "1001",
                Guid.NewGuid(),
                "CE211Q1",
                "CE 2011 Quarter 1",
                "01");
        }

    }
}
