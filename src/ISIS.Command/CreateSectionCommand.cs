using System;
using Ncqrs.Commanding;

namespace ISIS
{
    public class CreateSectionCommand : CommandBase
    {

        public Guid CourseId { get; set; }
        public Guid TermId { get; set; }
        public string SectionNumber { get; set; }

    }
}