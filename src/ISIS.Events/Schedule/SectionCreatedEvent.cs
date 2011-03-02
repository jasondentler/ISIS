using System;

namespace ISIS.Schedule
{
    public class SectionCreatedEvent
    {

        public Guid SectionId { get; set; }
        public Guid CourseId { get; set; }
        public Guid TermId { get; set; }
        public string SectionNumber { get; set; }

    }
}