using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ChangeSectionNumberCommand : CommandBase
    {

        public Guid SectionId { get;  set; }
        public string SectionNumber { get; set; }

    }

}