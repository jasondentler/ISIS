using System;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Section : AggregateRootMappedByConvention 
    {

        [Inject]
        private Section()
        {
        }

        public Section(Guid sectionId, Term term, Course course, string sectionNumber)
            : base(sectionId)
        {
            var termData = term.BuildMememto();
            var courseData = course.BuildMemento();

            if (string.IsNullOrEmpty(courseData.ApprovalNumber) &&
                string.IsNullOrEmpty(courseData.CIP))
                throw new InvalidStateException(
                    "Your attempt to create the section failed. Set approval number or CIP at the course level first.");

            ApplyEvent(new SectionCreatedEvent(
                sectionId, 
                courseData.Id,
                courseData.Rubric,
                courseData.CourseNumber,
                termData.Id,
                termData.Abbreviation,
                termData.Name,
                sectionNumber,
                termData.Start,
                termData.End,
                courseData.Title,
                courseData.CourseTypes,
                courseData.ApprovalNumber,
                courseData.CIP));
        }

        protected void OnCreated(SectionCreatedEvent @event)
        {
        }

    }
}
