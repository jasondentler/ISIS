using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCreatedEvent
        : JsonEventSerializationFixture<CreditSectionCreatedEvent>
    {

        protected override CreditSectionCreatedEvent GivenEvent()
        {
            return new CreditSectionCreatedEvent(
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
