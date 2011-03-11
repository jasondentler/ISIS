using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseTitleChangedEvent
        : JsonEventSerializationFixture<CourseTitleChangedEvent>
    {

        protected override CourseTitleChangedEvent GivenEvent()
        {
            return new CourseTitleChangedEvent(Guid.NewGuid(), "New Title");
        }

    }
}
