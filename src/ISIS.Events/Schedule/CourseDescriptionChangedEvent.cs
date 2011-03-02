using System;

namespace ISIS.Schedule
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
