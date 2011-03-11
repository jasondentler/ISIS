using System;

namespace ISIS.Schedule
{
    public class CourseTitleChangedEvent : IEvent 
    {
        public CourseTitleChangedEvent(Guid courseId, string title)
        {
            CourseId = courseId;
            Title = title;
        }

        public Guid CourseId { get; private set; }
        public string Title { get; private set; }
    }
}