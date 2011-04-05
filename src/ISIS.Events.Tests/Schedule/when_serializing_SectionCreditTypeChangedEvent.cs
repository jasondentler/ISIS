using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionCreditTypeChangedEvent
        : JsonEventSerializationFixture<SectionCreditTypeChangedEvent>
    {

        protected override SectionCreditTypeChangedEvent GivenEvent()
        {
            return new SectionCreditTypeChangedEvent(
                Guid.NewGuid(),
                CreditTypes.WorkforceFunded);
        }

    }
}
