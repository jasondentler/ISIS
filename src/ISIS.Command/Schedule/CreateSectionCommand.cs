using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class CreateSectionCommand : CommandBase 
    {

        public Guid SectionId { get; set; }
        public string SectionNumber { get; set; }
        public Guid CourseId { get; set; }
        public Guid TermId { get; set; }

    }
}
