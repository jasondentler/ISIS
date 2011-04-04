using System;

namespace ISIS.Schedule
{

    public class when_serializing_SectionTopicCodeRemovedEvent
        : JsonEventSerializationFixture<SectionTopicCodeRemovedEvent>
    {

        protected override SectionTopicCodeRemovedEvent GivenEvent()
        {
            return new SectionTopicCodeRemovedEvent(Guid.NewGuid());
        }

    }
}
