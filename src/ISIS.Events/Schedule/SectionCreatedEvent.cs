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
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Title { get; private set; }
        public IEnumerable<CourseTypes> CourseTypes { get; private set; }
        public string ApprovalNumber { get; private set; }
        public string CIP { get; private set; }

        public SectionCreatedEvent(
            Guid sectionId,
            Guid courseId,
            string rubric,
            string courseNumber,
            Guid termId,
            string termAbbreviation,
            string termName,
            String sectionNumber,
            DateTime startDate,
            DateTime endDate,
            string title,
            IEnumerable<CourseTypes> courseTypes,
            string approvalNumber,
            string cip)
        {
            CourseId = courseId;
            Rubric = rubric;
            CourseNumber = courseNumber;
            TermId = termId;
            TermAbbreviation = termAbbreviation;
            TermName = termName;
            SectionNumber = sectionNumber;
            StartDate = startDate;
            EndDate = endDate;
            Title = title;
            CourseTypes = courseTypes;
            ApprovalNumber = approvalNumber;
            CIP = cip;
            SectionId = sectionId;
        }

    }
}