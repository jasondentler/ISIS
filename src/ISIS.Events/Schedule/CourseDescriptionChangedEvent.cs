using System;

namespace ISIS.Schedule
{
    public class CourseDescriptionChangedEvent : IEvent 
    {
        public Guid CourseId { get; private set; }
        public string Description { get; private set; }

        public CourseDescriptionChangedEvent(Guid courseId, string description)
        {
            CourseId = courseId;
            Description = description;
        }
    }
}
