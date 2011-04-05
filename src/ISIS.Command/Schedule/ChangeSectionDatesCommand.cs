using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeSectionDatesCommand : CommandBase 
    {

        public Guid SectionId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
