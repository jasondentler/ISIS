using System;

namespace ISIS
{
    public class CourseList : IEntity
    {

        public Guid Id { get; set; }
        public string Rubric { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }

    }
}
