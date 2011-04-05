using System;

namespace ISIS.Schedule
{
    public class SectionCIPChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public string CIP { get; private set; }

        public SectionCIPChangedEvent(
            Guid sectionId,
            string cip)
        {
            SectionId = sectionId;
            CIP = cip;
        }
    }
}