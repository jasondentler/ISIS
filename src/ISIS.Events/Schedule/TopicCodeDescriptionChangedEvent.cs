using System;

namespace ISIS.Schedule
{
    public class TopicCodeDescriptionChangedEvent : IEvent 
    {
        public Guid TopicCodeId { get; private set; }
        public string Description { get; private set; }
        public string PreviousDescription { get; private set; }

        public TopicCodeDescriptionChangedEvent(
            Guid topicCodeId,
            string description,
            string previousDescription)
        {
            TopicCodeId = topicCodeId;
            Description = description;
            PreviousDescription = previousDescription;
        }
    }
}
