using System;

namespace ISIS.Schedule
{
    public class CourseTopicCodeChangedEvent : IEvent 
    {

        public Guid CourseId { get; private set; }
        public Guid TopicCodeId { get; set; }
        public string TopicCodeAbbreviation { get; set; }
        public string TopicCodeDescription { get; set; }

        public CourseTopicCodeChangedEvent(Guid courseId, TopicCodeMemento memento)
        {
            CourseId = courseId;
            TopicCodeId = memento.Id;
            TopicCodeAbbreviation = memento.Abbreviation;
            TopicCodeDescription = memento.Description;
        }

    }
}
