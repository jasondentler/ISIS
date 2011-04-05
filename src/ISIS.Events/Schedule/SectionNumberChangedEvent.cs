using System;

namespace ISIS.Schedule
{
    public class SectionNumberChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public string SectionNumber { get; private set; }

        public SectionNumberChangedEvent(
            Guid sectionId,
            String sectionNumber)
        {
            SectionNumber = sectionNumber;
            SectionId = sectionId;
        }

    }
}