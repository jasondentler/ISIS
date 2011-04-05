using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    public class SectionCreatedEvent : IEvent 
    {

        public Guid SectionId { get; private set; }
        public Guid CourseId { get; private set; }
        public string Rubric { get; private set; }
        public string CourseNumber { get; private set; }
        public Guid TermId { get; private set; }
        public string TermAbbreviation { get; private set; }
        public string TermName { get; private set; }
        public string SectionNumber { get; private set; }

        public SectionCreatedEvent(
            Guid sectionId,
            Guid courseId,
            string rubric,
            string courseNumber,
            Guid termId,
            string termAbbreviation,
            string termName,
            String sectionNumber)
        {
            CourseId = courseId;
            Rubric = rubric;
            CourseNumber = courseNumber;
            TermId = termId;
            TermAbbreviation = termAbbreviation;
            TermName = termName;
            SectionNumber = sectionNumber;
            SectionId = sectionId;
        }

    }
}