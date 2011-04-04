using System;

namespace ISIS.Schedule
{

    public class when_serializing_TermCreatedEvent
        : JsonEventSerializationFixture<TermCreatedEvent>
    {

        protected override TermCreatedEvent GivenEvent()
        {
            return new TermCreatedEvent(Guid.NewGuid(),
                                        "Fall 2011",
                                        "211FA",
                                        DateTime.Today.AddMonths(-2),
                                        DateTime.Today.AddMonths(2));

        }

    }
}
