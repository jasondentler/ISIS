using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class CreateSectionCommand : CommandBase
    {

        public Guid CourseId { get; set; }
        public Guid TermId { get; set; }
        public string Number { get; set; }

    }
}