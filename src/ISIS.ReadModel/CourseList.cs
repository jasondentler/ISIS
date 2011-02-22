using System;

namespace ISIS
{
    public class CourseList : IEntity
    {

        [Id]
        public Guid CourseId { get; set; }
        public string Rubric { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }

    }

}
