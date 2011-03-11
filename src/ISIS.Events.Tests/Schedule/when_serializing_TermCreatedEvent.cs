using System;

namespace ISIS.Schedule
{

    public class when_serializing_TermCreatedEvent
        : JsonEventSerializationFixture<TermCreatedEvent>
    {

        protected override TermCreatedEvent GivenEvent()
        {
            return new TermCreatedEvent();
        }

    }
}
