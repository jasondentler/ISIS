using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseActivatedEvent 
        : JsonEventSerializationFixture<CourseActivatedEvent>
    {

        protected override CourseActivatedEvent GivenEvent()
        {
            return new CourseActivatedEvent(Guid.NewGuid(), DateTime.Now);
        }

    }
}
