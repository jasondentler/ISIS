using System;

namespace ISIS
{
    public class CourseCreatedEvent 
    {

        public CourseCreatedEvent(Guid courseId, string rubric, string number)
        {
            CourseId = courseId;
            Rubric = rubric;
            Number = number;
        }

        public Guid CourseId { get; private set; }
        public string Rubric { get; private set; }
        public string Number { get; private set; }

    }
}
