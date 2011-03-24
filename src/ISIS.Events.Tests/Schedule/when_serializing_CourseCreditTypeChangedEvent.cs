using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseCreditTypeChangedEvent
        : JsonEventSerializationFixture<CourseCreditTypeChangedEvent>
    {

        protected override CourseCreditTypeChangedEvent GivenEvent()
        {
            return new CourseCreditTypeChangedEvent(Guid.NewGuid(), CreditTypes.SpecialInterests);
        }

    }
}
