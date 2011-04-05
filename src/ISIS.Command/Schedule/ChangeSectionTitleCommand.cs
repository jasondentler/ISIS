using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ChangeSectionTitleCommand : CommandBase
    {

        public Guid SectionId { get;  set; }
        public string NewTitle { get; set; }

    }

}