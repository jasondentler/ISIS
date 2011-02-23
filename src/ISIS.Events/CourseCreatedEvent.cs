using System;

namespace ISIS
{
    public class CourseCreatedEvent 
    {

        public CourseCreatedEvent(Guid courseId, string rubric, string number, string title)
        {
            CourseId = courseId;
            Rubric = rubric;
            Number = number;
            Title = title;
        }

        public Guid CourseId { get; private set; }
        public string Rubric { get; private set; }
        public string Number { get; private set; }
        public string Title { get; private set; }

    }
}
