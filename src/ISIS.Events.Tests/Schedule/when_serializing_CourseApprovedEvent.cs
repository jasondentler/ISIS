using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseApprovedEvent
        : JsonEventSerializationFixture<CourseApprovedEvent>
    {

        protected override CourseApprovedEvent GivenEvent()
        {
            return new CourseApprovedEvent(
                Guid.NewGuid(), "Me");
        }

    }
}
