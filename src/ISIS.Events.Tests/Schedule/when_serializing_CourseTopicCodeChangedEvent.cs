using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseTopicCodeChangedEvent
        : JsonEventSerializationFixture<CourseTopicCodeChangedEvent>
    {

        protected override CourseTopicCodeChangedEvent GivenEvent()
        {
            return new CourseTopicCodeChangedEvent(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "Abbreviation",
                "Description"
                );
        }

    }
}
