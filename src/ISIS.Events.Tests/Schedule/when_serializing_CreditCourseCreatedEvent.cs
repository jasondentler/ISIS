using System;

namespace ISIS.Schedule
{

    public class when_serializing_CreditCourseCreatedEvent
        : JsonEventSerializationFixture<CreditCourseCreatedEvent>
    {

        protected override CreditCourseCreatedEvent GivenEvent()
        {
            return new CreditCourseCreatedEvent(
                Guid.NewGuid(), "BIOL", "1301");
        }

    }
}
