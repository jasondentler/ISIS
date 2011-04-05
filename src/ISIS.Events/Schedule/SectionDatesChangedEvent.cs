using System;

namespace ISIS.Schedule
{
    public class SectionDatesChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }

        public DateTime Start { get; private set; }

        public DateTime End { get; private set; }

        public SectionDatesChangedEvent(
            Guid sectionId,
            DateTime start,
            DateTime end)
        {
            SectionId = sectionId;
            Start = start;
            End = end;
        }
    }
}
