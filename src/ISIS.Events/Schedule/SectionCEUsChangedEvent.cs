using System;

namespace ISIS.Schedule
{
    public class SectionCEUsChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public decimal CEUs { get; private set; }

        public SectionCEUsChangedEvent(Guid sectionId, decimal ceus)
        {
            SectionId = sectionId;
            CEUs = ceus;
        }
    }
}
