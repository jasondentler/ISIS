using System;

namespace ISIS
{
    public class CourseCreatedEvent 
    {

        public Guid CourseId { get; set; }
        public string Rubric { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }

    }
}
