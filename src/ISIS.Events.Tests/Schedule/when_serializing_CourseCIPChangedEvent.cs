using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseCIPChangedEvent 
        : JsonEventSerializationFixture<CourseCIPChangedEvent>
    {

        protected override CourseCIPChangedEvent GivenEvent()
        {
            return new CourseCIPChangedEvent(Guid.NewGuid(), "123456");
        }

    }
}
