using System;
using Ncqrs.Domain;

namespace ISIS
{
    public class Section : AggregateRootMappedByConvention
    {

        private Section()
        {
        }

        public Section(Guid courseId, Guid termId, string sectionNumber)
        {
            var e = new SectionCreatedEvent()
                        {
                            CourseId = courseId,
                            TermId = termId,
                            SectionNumber = sectionNumber
                        };
            ApplyEvent(e);
        }

        protected void OnSectionCreated(SectionCreatedEvent @event)
        {
        }

    }
}
