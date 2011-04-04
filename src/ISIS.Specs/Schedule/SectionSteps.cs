using System;
using System.Linq;
using Ncqrs;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Storage;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class SectionSteps
    {

        [Given(@"I have created a section (.*) from the course and term")]
        public void GivenIHaveCreatedASectionFromTheCourseAndTerm(
            string sectionNumber)
        {
            var courseId = DomainHelper.GetId<Course>();
            var courseData = GetCourseData(courseId);

            var termId = DomainHelper.GetId<Term>();
            var termData = GetTermMemento(termId);

            var id = Guid.NewGuid();
            DomainHelper.SetId<Section>(id, termId.ToString(), courseId.ToString(), sectionNumber);

            DomainHelper.GivenEvent(
                id,
                new SectionCreatedEvent(
                    id,
                    courseId,
                    courseData.Rubric,
                    courseData.CourseNumber,
                    termId,
                    termData.Abbreviation,
                    termData.Name,
                    sectionNumber,
                    termData.Start,
                    termData.End,
                    courseData.Title,
                    courseData.CourseTypes.ToArray(),
                    courseData.ApprovalNumber,
                    courseData.CIP));
        }

        private CourseMemento GetCourseData(Guid courseId)
        {
            return GetAggregate<Course>(courseId).BuildMemento();
        }

        private TermMemento GetTermMemento(Guid termId)
        {
            return GetAggregate<Term>(termId).BuildMememto();
        }

        private TAggregateRoot GetAggregate<TAggregateRoot>(Guid id)
            where TAggregateRoot : AggregateRoot
        {
            var store = NcqrsEnvironment.Get<IEventStore>();
            var stream = store.ReadFrom(id, 0, long.MaxValue);

            var arCreationStrategy = NcqrsEnvironment.Get<IAggregateRootCreationStrategy>();
            var arSnapshotter = NcqrsEnvironment.Get<IAggregateSnapshotter>();
            var repo = new DomainRepository(arCreationStrategy, arSnapshotter);
            return (TAggregateRoot) repo.Load(typeof (TAggregateRoot), null, stream);
        }


        [When(@"I create section ([^\s]+) from the course and term")]
        public void WhenICreateSection(
            string sectionNumber)
        {
            var courseId = DomainHelper.GetId<Course>();
            var termId = DomainHelper.GetId<Term>();

            var id = Guid.NewGuid();
            DomainHelper.SetId<Section>(id, termId.ToString(), courseId.ToString(), sectionNumber);

            var cmd = new CreateSectionCommand()
                          {
                              SectionId = id,
                              CourseId = courseId,
                              TermId = termId,
                              SectionNumber = sectionNumber
                          };
            DomainHelper.WhenExecuting(cmd);
        }


        [Then(@"the section's term is (.*)")]
        public void ThenTheSectionSTermIs(
            string termAbbreviation)
        {
            var termId = DomainHelper.GetId<Term>(termAbbreviation);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.TermId, Is.EqualTo(termId));
        }

        [Then(@"the section's term name is (.*)")]
        public void ThenTheSectionSTermNameIs(
            string termName)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.TermName, Is.EqualTo(termName));
        }


        [Then(@"the section's term abbreviation is (.*)")]
        public void ThenTheSectionSTermAbbreviationIs(
            string termAbbreviation)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.TermAbbreviation, Is.EqualTo(termAbbreviation));
        }

        [Then(@"the section's course is ([^\s]+) ([^\s]+)")]
        public void ThenTheSectionSCourseIs(
            string rubric,
            string courseNumber)
        {
            var courseId = DomainHelper.GetId<Course>(rubric, courseNumber);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.CourseId, Is.EqualTo(courseId));
        }



        [Then(@"the section's rubric is (.*)")]
        public void ThenTheSectionSRubricIs(
            string rubric)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.Rubric, Is.EqualTo(rubric));
        }

        [Then(@"the section's course number is (.*)")]
        public void ThenTheSectionSCourseNumberIs(
            string courseNumber)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.CourseNumber, Is.EqualTo(courseNumber));
        }

        [Then(@"the section's section number is (.*)")]
        public void ThenTheSectionSSectionNumberIs(
            string sectionNumber)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.SectionNumber, Is.EqualTo(sectionNumber));
        }


        [Then(@"the section's start date is (.*)")]
        public void ThenTheSectionSStartDateIs(
            string startDateString)
        {
            var startDate = DateTime.Parse(startDateString);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.StartDate, Is.EqualTo(startDate));
        }

        [Then(@"the section's end date is (.*)")]
        public void ThenTheSectionSEndDateIs(
            string endDateString)
        {
            var endDate = DateTime.Parse(endDateString);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.EndDate, Is.EqualTo(endDate));
        }

        [Then(@"the section's title is ""(.*)""")]
        public void ThenTheSectionSTitleIs(
            string sectionTitle)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.Title, Is.EqualTo(sectionTitle));
        }

        [Then(@"the section's course type is (.*)")]
        public void ThenTheSectionSCourseTypeIs(
            string sectionCourseType)
        {
            var courseTypes = CourseTypeSteps.ParseCourseTypes(sectionCourseType);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.CourseTypes, Is.EqualTo(courseTypes));
        }

        [Then(@"the section's approval number is (.*)")]
        public void ThenTheSectionSApprovalNumberIs(
            string approvalNumber)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.ApprovalNumber, Is.EqualTo(approvalNumber));
        }

        [Then(@"the section's CIP is (.*)")]
        public void ThenTheSectionSCIPIs(
            string cip)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.CIP, Is.EqualTo(cip));
        }

   }
}
