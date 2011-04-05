using System;

namespace ISIS.Schedule
{
    public class SectionDatesRemovedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }

        public SectionDatesRemovedEvent(
            Guid sectionId)
        {
            SectionId = sectionId;
        }
    }
}
