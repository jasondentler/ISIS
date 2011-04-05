using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionApprovalNumberChangedEvent
        : JsonEventSerializationFixture<SectionApprovalNumberChangedEvent>
    {

        protected override SectionApprovalNumberChangedEvent GivenEvent()
        {
            return new SectionApprovalNumberChangedEvent(
                Guid.NewGuid(), "1234567890");
        }

    }
}
