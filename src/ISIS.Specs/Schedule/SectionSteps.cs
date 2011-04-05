using System;
using System.Linq;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;
using Ncqrs.Domain.Storage;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class SectionSteps
    {

        [TechTalk.SpecFlow.Given(@"I have set the section location to ([^\s]+) (.*)")]
        public void GivenIHaveSetTheLocationTo(
            string locationAbbreviation,
            string locationName)
        {
            var id = DomainHelper.GetId<Section>();
            var locationId = DomainHelper.GetId<Location>(locationAbbreviation);

            var tdcjTopicCode = CourseTopicCodeSteps.GetTDCTopicCodeId();

            DomainHelper.GivenEvent<Section>(
                new SectionLocationChangedEvent(
                    id,
                    locationId,
                    locationAbbreviation,
                    locationName));

            switch (locationAbbreviation)
            {
                case "CLEM":
                case "CV":
                case "DAR":
                case "J1":
                case "J2":
                case "J3":
                case "R1":
                case "R2":
                case "TER":
                    DomainHelper.GivenEvent(
                        id,
                        new SectionTopicCodeChangedEvent(
                            id,
                            tdcjTopicCode,
                            "A",
                            "Academic TDC Course Code"));
                    break;
            }

        }


        [TechTalk.SpecFlow.Given(@"I have created a section (.*) from the course and term")]
        public void GivenIHaveCreatedASectionFromTheCourseAndTerm(
            string sectionNumber)
        {
            var courseId = DomainHelper.GetId<Course>();
            var termId = DomainHelper.GetId<Term>();
            GivenIHaveCreatedASectionFromTheCourseAndTerm(
                sectionNumber, courseId, termId);
        }

        [TechTalk.SpecFlow.Given(@"I have created a section (.*) from the course with term (.*)")]
        public void GivenIHaveCreatedASectionFromTheCourseWithTerm(
            string sectionNumber,
            string termAbbreviation)
        {
            var courseId = DomainHelper.GetId<Course>();
            var termId = DomainHelper.GetId<Term>(termAbbreviation);
            GivenIHaveCreatedASectionFromTheCourseAndTerm(
                sectionNumber,
                courseId,
                termId);
        }


        public void GivenIHaveCreatedASectionFromTheCourseAndTerm(
            string sectionNumber,
            Guid courseId, 
            Guid termId)
        {

            var id = Guid.NewGuid();
            DomainHelper.SetId<Section>(id, termId.ToString(), courseId.ToString(), sectionNumber);

            var cmd = new CreateSectionCommand()
                          {
                              CourseId = courseId,
                              SectionId = id,
                              SectionNumber = sectionNumber,
                              TermId = termId
                          };

            var commandService = NcqrsEnvironment.Get<ICommandService>();
            using (var ctx = new EventContext())
            {
                commandService.Execute(cmd);
                foreach (var @event in ctx.Events.Select(e => e.Payload))
                {
                    Console.Write("\tGiven ");
                    DomainHelper.WriteOutObject(@event);
                }
            }
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
            var termId = DomainHelper.GetId<Term>();
            CreateSection(sectionNumber, termId);
        }

        [When(@"I create section ([^\s]+) from the course with term ([^\s]+)")]
        public void WhenICreateSectionFromTheCourseWithTerm(
            string sectionNumber,
            string termAbbreviation)
        {
            var termId = DomainHelper.GetId<Term>(termAbbreviation);
            CreateSection(sectionNumber, termId);
        }

        private void CreateSection(
            string sectionNumber,
            Guid termId)
        {
            var courseId = DomainHelper.GetId<Course>();

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

        [When(@"I change the section location to ([^\s]+)")]
        public void WhenIChangeTheSectionLocationTo(
            string locationAbbreviation)
        {
            var locationId = DomainHelper.GetId<Location>(locationAbbreviation);

            var tdcjTopiCodeId = CourseTopicCodeSteps.GetTDCTopicCodeId();

            var cmd = new ChangeSectionLocationCommand()
                          {
                              LocationId = locationId,
                              TDCJTopicCodeId = tdcjTopiCodeId,
                              SectionId = DomainHelper.GetId<Section>()
                          };
            DomainHelper.WhenExecuting(cmd);
        }



        [TechTalk.SpecFlow.Then(@"the section's term is (.*)")]
        public void ThenTheSectionSTermIs(
            string termAbbreviation)
        {
            var termId = DomainHelper.GetId<Term>(termAbbreviation);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.TermId, Is.EqualTo(termId));
        }

        [TechTalk.SpecFlow.Then(@"the section's term name is (.*)")]
        public void ThenTheSectionSTermNameIs(
            string termName)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.TermName, Is.EqualTo(termName));
        }


        [TechTalk.SpecFlow.Then(@"the section's term abbreviation is (.*)")]
        public void ThenTheSectionSTermAbbreviationIs(
            string termAbbreviation)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.TermAbbreviation, Is.EqualTo(termAbbreviation));
        }

        [TechTalk.SpecFlow.Then(@"the section's course is ([^\s]+) ([^\s]+)")]
        public void ThenTheSectionSCourseIs(
            string rubric,
            string courseNumber)
        {
            var courseId = DomainHelper.GetId<Course>(rubric, courseNumber);

            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.CourseId, Is.EqualTo(courseId));
        }



        [TechTalk.SpecFlow.Then(@"the section's rubric is (.*)")]
        public void ThenTheSectionSRubricIs(
            string rubric)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.Rubric, Is.EqualTo(rubric));
        }

        [TechTalk.SpecFlow.Then(@"the section's course number is (.*)")]
        public void ThenTheSectionSCourseNumberIs(
            string courseNumber)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.CourseNumber, Is.EqualTo(courseNumber));
        }

        [TechTalk.SpecFlow.Then(@"the section's section number is (.*)")]
        public void ThenTheSectionSSectionNumberIs(
            string sectionNumber)
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e.SectionNumber, Is.EqualTo(sectionNumber));
        }

        [TechTalk.SpecFlow.Then(@"the section's start date is not set")]
        [TechTalk.SpecFlow.Then(@"the section's end date is not set")]
        public void ThenTheSectionsDatesAreNotSet()
        {
            var datesChangedEvents = DomainHelper
                .GetEvents<SectionDatesChangedEvent>()
                .ToArray();
            Assert.That(datesChangedEvents, Is.Empty);
        }

        [TechTalk.SpecFlow.Then(@"the section's start date is (\d.*)")]
        public void ThenTheSectionSStartDateIs(
            string startDateString)
        {
            var startDate = DateTime.Parse(startDateString);

            var e = DomainHelper.GetEvent<SectionDatesChangedEvent>();
            Assert.That(e.Start, Is.EqualTo(startDate));
        }

        [TechTalk.SpecFlow.Then(@"the section's end date is (\d.*)")]
        public void ThenTheSectionSEndDateIs(
            string endDateString)
        {
            var endDate = DateTime.Parse(endDateString);

            var e = DomainHelper.GetEvent<SectionDatesChangedEvent>();
            Assert.That(e.End, Is.EqualTo(endDate));
        }

        [TechTalk.SpecFlow.Then(@"the section's title is ""(.*)""")]
        public void ThenTheSectionSTitleIs(
            string sectionTitle)
        {
            var e = DomainHelper.GetEvent<SectionTitleChangedEvent>();
            Assert.That(e.Title, Is.EqualTo(sectionTitle));
        }

        [TechTalk.SpecFlow.Then(@"the section's course type is (.*)")]
        public void ThenTheSectionSCourseTypeIs(
            string sectionCourseType)
        {
            var courseTypes = CourseTypeSteps.ParseCourseTypes(sectionCourseType);

            var e = DomainHelper.GetEvents<SectionCourseTypeAddedEvent>()
                .Last();

            Assert.That(e.CourseTypeAdded, Is.EqualTo(courseTypes.Single()));
        }

        [TechTalk.SpecFlow.Then(@"the section's current course type is (.*)")]
        public void ThenTheSectionSCurrentCourseTypeIs(
            string sectionCourseType)
        {
            var courseTypes = CourseTypeSteps.ParseCourseTypes(sectionCourseType);

            dynamic @event = DomainHelper
                .GetEvents()
                .Select(e => e.Payload)
                .Where(e => e is SectionCourseTypeAddedEvent)
                .Last();

            Assert.That(@event.CurrentCourseTypes, Is.EqualTo(courseTypes));
        }


        [TechTalk.SpecFlow.Then(@"the section's approval number is (.*)")]
        public void ThenTheSectionSApprovalNumberIs(
            string approvalNumber)
        {
            var e = DomainHelper.GetEvent<SectionApprovalNumberChangedEvent>();
            Assert.That(e.ApprovalNumber, Is.EqualTo(approvalNumber));
        }

        [TechTalk.SpecFlow.Then(@"the section's CIP is (.*)")]
        public void ThenTheSectionSCIPIs(
            string cip)
        {
            var e = DomainHelper.GetEvent<SectionCIPChangedEvent>();
            Assert.That(e.CIP, Is.EqualTo(cip));
        }

        [TechTalk.SpecFlow.Then(@"the section location is (.*)")]
        public void ThenTheSectionLocationIs(
            string locationAbbreviation)
        {
            var locationId = DomainHelper.GetId<Location>(locationAbbreviation);

            var e = DomainHelper.GetEvent<SectionLocationChangedEvent>();
            Assert.That(e.LocationId, Is.EqualTo(locationId));
        }

        [TechTalk.SpecFlow.Then(@"the section location abbreviation is ([^\s]+)")]
        public void ThenTheSectionLocationAbbreviationIs(
            string locationAbbreviation)
        {
            var e = DomainHelper.GetEvent<SectionLocationChangedEvent>();
            Assert.That(e.LocationAbbreviation, Is.EqualTo(locationAbbreviation));
        }

        [TechTalk.SpecFlow.Then(@"the section location name is (.*)")]
        public void ThenTheSectionLocationNameIs(
            string locationName)
        {
            var e = DomainHelper.GetEvent<SectionLocationChangedEvent>();
            Assert.That(e.LocationName, Is.EqualTo(locationName));
        }


        [TechTalk.SpecFlow.Then(@"the topic code is blank")]
        public void ThenTheTopicCodeIsBlank()
        {
            var e = DomainHelper.GetEvent<SectionTopicCodeRemovedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [TechTalk.SpecFlow.Then(@"the section's topic code is ([A-Z0-9]+)")]
        public void ThenTheSectionSTopicCodeIs(
            string topicCodeAbbreviation)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>(topicCodeAbbreviation);

            var e = DomainHelper.GetEvent<SectionTopicCodeChangedEvent>();

            Assert.That(e, Is.Not.Null);
            Assert.That(e.TopicCodeId, Is.EqualTo(topicCodeId));
            Assert.That(e.TopicCodeAbbreviation, Is.EqualTo(topicCodeAbbreviation));
        }

        [TechTalk.SpecFlow.Then(@"the topic code is ([A-Z0-9]+) (.*)")]
        public void ThenTheTopicCodeIs(
            string topicCodeAbbreviation,
            string topicCodeDescription)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>(topicCodeAbbreviation);

            var e = DomainHelper.GetEvent<SectionTopicCodeChangedEvent>();

            Assert.That(e, Is.Not.Null);
            Assert.That(e.TopicCodeId, Is.EqualTo(topicCodeId));
            Assert.That(e.TopicCodeAbbreviation, Is.EqualTo(topicCodeAbbreviation));
            Assert.That(e.TopicCodeDescription, Is.EqualTo(topicCodeDescription));
        }

        [TechTalk.SpecFlow.Then(@"the section is created")]
        public void ThenTheSectionIsCreated()
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [TechTalk.SpecFlow.Then(@"the section's credit type is (.*)")]
        public void ThenTheSectionSCreditTypeIs(
            string creditTypeString)
        {
            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);

            var e = DomainHelper.GetEvent<SectionCreditTypeChangedEvent>();
            Assert.That(e.CreditType, Is.EqualTo(creditType));
        }

        [TechTalk.SpecFlow.Then(@"the section's status is pending")]
        public void ThenTheSectionSStatusIsPending()
        {
            var e = DomainHelper.GetEvent<SectionMadePendingEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [TechTalk.SpecFlow.Then(@"the location is blank")]
        public void ThenTheLocationIsBlank()
        {
            var events = DomainHelper
                .GetEvents<SectionLocationChangedEvent>()
                .ToArray();

            Assert.That(events, Is.Empty);
        }

        [TechTalk.SpecFlow.Then(@"the section's CEUs is ([^\s]+)")]
        [TechTalk.SpecFlow.Then(@"the section's CEUs are ([^\s]+)")]
        public void ThenTheSectionSCEUsIs(
            string ceusString)
        {
            var ceus = decimal.Parse(ceusString);
            var e = DomainHelper.GetEvent<SectionCEUsChangedEvent>();
            Assert.That(e.CEUs, Is.EqualTo(ceus));
        }
        
    }
}
