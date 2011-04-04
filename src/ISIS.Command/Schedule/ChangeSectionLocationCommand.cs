using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ChangeSectionLocationCommand : CommandBase
    {

        public Guid SectionId { get;  set; }
        public Guid LocationId { get; set; }

    }

}