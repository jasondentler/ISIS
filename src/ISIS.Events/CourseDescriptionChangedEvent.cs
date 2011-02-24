using System;

namespace ISIS
{
    public class CourseDescriptionChangedEvent
    {
        public Guid CourseId { get; private set; }
        public string Description { get; private set; }

        public CourseDescriptionChangedEvent(Guid eventSourceId, string description)
        {
            CourseId = eventSourceId;
            Description = description;
        }
    }
}
