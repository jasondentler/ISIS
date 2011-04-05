using System;

namespace ISIS.Schedule
{
    public class SectionTitleChangedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public string PreviousTitle { get; private set; }
        public string Title { get; private set; }

        public SectionTitleChangedEvent(
            Guid sectionId,
            string previousTitle,
            string title)
        {
            SectionId = sectionId;
            PreviousTitle = previousTitle;
            Title = title;
        }
    }
}
