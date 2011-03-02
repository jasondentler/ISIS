using System;

namespace ISIS.Schedule
{
    /// <summary>
    /// Used for set validation of courses
    /// </summary>
    public class CourseSet : IEntity
    {

        [Id]
        public Guid CourseId { get; set; }

        public string Rubric { get; set; }

        public string Number { get; set; }

    }
}
