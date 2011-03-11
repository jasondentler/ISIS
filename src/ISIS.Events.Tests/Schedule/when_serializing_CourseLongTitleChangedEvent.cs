using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseLongTitleChangedEvent
        : JsonEventSerializationFixture<CourseLongTitleChangedEvent>
    {

        protected override CourseLongTitleChangedEvent GivenEvent()
        {
            return new CourseLongTitleChangedEvent(Guid.NewGuid(), "New long title goes here");
        }

    }
}
