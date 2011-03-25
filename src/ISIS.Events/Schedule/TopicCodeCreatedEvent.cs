using System;

namespace ISIS.Schedule
{
    public class TopicCodeCreatedEvent : IEvent 
    {
        public Guid TopicCodeId { get; private set; }
        public string Abbreviation { get; private set; }
        public string Description { get; private set; }

        public TopicCodeCreatedEvent(
            Guid topicCodeId,
            string abbreviation,
            string description)
        {
            TopicCodeId = topicCodeId;
            Abbreviation = abbreviation;
            Description = description;
        }
    }
}
