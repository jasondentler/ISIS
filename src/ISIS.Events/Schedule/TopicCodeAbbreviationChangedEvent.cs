using System;

namespace ISIS.Schedule
{
    public class TopicCodeAbbreviationChangedEvent : IEvent 
    {
        public Guid TopicCodeId { get; private set; }
        public string Abbreviation { get; private set; }
        public string PreviousAbbreviation { get; private set; }

        public TopicCodeAbbreviationChangedEvent(
            Guid topicCodeId,
            string abbreviation,
            string previousAbbreviation)
        {
            TopicCodeId = topicCodeId;
            Abbreviation = abbreviation;
            PreviousAbbreviation = previousAbbreviation;
        }
    }
}
