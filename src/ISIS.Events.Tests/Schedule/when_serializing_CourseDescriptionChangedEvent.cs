using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseDescriptionChangedEvent
        : JsonEventSerializationFixture<CourseDescriptionChangedEvent>
    {

        protected override CourseDescriptionChangedEvent GivenEvent()
        {
            return new CourseDescriptionChangedEvent(Guid.NewGuid(), "New description goes here");
        }

    }
}
