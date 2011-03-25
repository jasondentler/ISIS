using System;

namespace ISIS.Schedule
{
    public class CourseTopicCodeChangedEvent : IEvent 
    {

        public Guid CourseId { get; private set; }
        public Guid TopicCodeId { get; set; }
        public string TopicCodeAbbreviation { get; set; }
        public string TopicCodeDescription { get; set; }

        public CourseTopicCodeChangedEvent(
            Guid courseId, 
            Guid topicCodeId,
            string topicCodeAbbreviation,
            string topicCodeDescription)
        {
            CourseId = courseId;
            TopicCodeId = topicCodeId;
            TopicCodeAbbreviation = topicCodeAbbreviation;
            TopicCodeDescription = topicCodeDescription;
        }

        public CourseTopicCodeChangedEvent(Guid courseId, TopicCodeMemento memento)
            : this(courseId, memento.Id, memento.Abbreviation, memento.Description)
        {
        }

    }
}
