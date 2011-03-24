using System;

namespace ISIS.Schedule
{

    public class when_serializing_CreditCourseCreatedEvent
        : JsonEventSerializationFixture<CourseCreatedEvent>
    {

        protected override CourseCreatedEvent GivenEvent()
        {
            return new CourseCreatedEvent(
                Guid.NewGuid(), "BIOL", "1301");
        }

    }
}
