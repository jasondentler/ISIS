using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class CreateTermCommand : CommandBase
    {

        public Guid TermId { get; set; }
        public string Abbreviation { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}