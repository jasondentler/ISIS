using System;

namespace ISIS
{
    public class CourseTitleChangedEvent 
    {
        public Guid CourseId { get; set; }
        public string NewTitle { get; set; }
    }
}