using System;

namespace ISIS.Schedule
{
    public class CourseList : IEntity
    {

        [Id]
        public Guid CourseId { get; set; }

        public string Rubric { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} : {2}", Rubric, Number, Title);
        }

    }

}
