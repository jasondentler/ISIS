using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ChangeSectionCreditTypeCommand : CommandBase
    {

        public Guid SectionId { get; set; }
        public CreditTypes CreditType { get; set; }

    }

}
