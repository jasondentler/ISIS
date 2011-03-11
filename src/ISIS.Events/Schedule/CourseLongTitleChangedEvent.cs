using System;

namespace ISIS.Schedule
{
    public class CourseLongTitleChangedEvent : IEvent 
    {

        public CourseLongTitleChangedEvent(Guid courseId, string longTitle)
        {
            CourseId = courseId;
            LongTitle = longTitle;
        }

        public Guid CourseId { get; private set; }
        public string LongTitle { get; private set; }
    }
}