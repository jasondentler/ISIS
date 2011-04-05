using System;

namespace ISIS.Schedule
{
    public class SectionMadePendingEvent : IEvent 
    {

        public Guid SectionId { get; private set; }

        public SectionMadePendingEvent(
            Guid sectionId)
        {
            SectionId = sectionId;
        }
    }
}