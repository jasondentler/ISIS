using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeSectionCEUsCommand : CommandBase 
    {

        public Guid SectionId { get; set; }
        public decimal CEUs { get; set; }

    }
}
