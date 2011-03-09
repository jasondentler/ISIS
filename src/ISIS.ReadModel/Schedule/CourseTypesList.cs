using System;

namespace ISIS.Schedule
{
    public class CourseTypesList : IEntity
    {

        [Id]
        public Guid Id { get; set; }

        public Guid CourseId { get; set; }

        public CourseTypes CourseType { get; set; }

    }
}
