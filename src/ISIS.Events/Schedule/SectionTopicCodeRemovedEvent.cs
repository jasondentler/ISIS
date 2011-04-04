using System;

namespace ISIS.Schedule
{
    public class SectionTopicCodeRemovedEvent : IEvent 
    {
        public Guid SectionId { get; private set; }

        public SectionTopicCodeRemovedEvent(
            Guid sectionId)
        {
            SectionId = sectionId;
        }
    }
}
