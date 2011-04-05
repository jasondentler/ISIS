using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ChangeSectionTermCommand : CommandBase
    {

        public Guid SectionId { get;  set; }
        public Guid TermId { get; set; }

    }

}