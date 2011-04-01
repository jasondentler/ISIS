using System;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Section : AggregateRoot 
    {

        [Inject]
        private Section()
        {
        }

        public Section(Guid sectionId, Term term, Course course, string sectionNumber)
            : base(sectionId)
        {

        }
    }
}
