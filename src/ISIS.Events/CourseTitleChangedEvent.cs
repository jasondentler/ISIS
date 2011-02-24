using System;

namespace ISIS
{
    public class CourseTitleChangedEvent 
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