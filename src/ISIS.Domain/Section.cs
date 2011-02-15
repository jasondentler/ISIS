using System;
using Ncqrs.Domain;

namespace ISIS
{
    public class Section : AggregateRootMappedByConvention
    {

        private Section()
        {
        }

        public Section(Guid courseId, string sectionNumber)
        {
            
        }

    }
}
