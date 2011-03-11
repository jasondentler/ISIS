using System;

namespace ISIS.Schedule
{

    public class when_serializing_CourseApprovalNumberChangedEvent 
        : JsonEventSerializationFixture<CourseApprovalNumberChangedEvent>
    {

        protected override CourseApprovalNumberChangedEvent GivenEvent()
        {
            return new CourseApprovalNumberChangedEvent(Guid.NewGuid(), "1234567890");
        }

    }
}
