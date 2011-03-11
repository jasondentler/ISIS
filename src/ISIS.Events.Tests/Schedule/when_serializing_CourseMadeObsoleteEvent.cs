using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseMadeObsoleteEvent
        : JsonEventSerializationFixture<CourseMadeObsoleteEvent>
    {

        protected override CourseMadeObsoleteEvent GivenEvent()
        {
            return new CourseMadeObsoleteEvent(Guid.NewGuid());
        }

    }
}
