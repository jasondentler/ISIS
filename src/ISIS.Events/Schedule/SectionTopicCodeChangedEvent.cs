using System;

namespace ISIS.Schedule
{
    public class SectionTopicCodeChangedEvent : IEvent 
    {
        public Guid SectionId { get; private set; }
        public Guid TopicCodeId { get; private set; }
        public string TopicCodeAbbreviation { get; private set; }
        public string TopicCodeDescription { get; private set; }

        public SectionTopicCodeChangedEvent(
            Guid sectionId,
            Guid topicCodeId, 
            string topicCodeAbbreviation,
            string topicCodeDescription)
        {
            SectionId = sectionId;
            TopicCodeId = topicCodeId;
            TopicCodeAbbreviation = topicCodeAbbreviation;
            TopicCodeDescription = topicCodeDescription;
        }
    }
}
