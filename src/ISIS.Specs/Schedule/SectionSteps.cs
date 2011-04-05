using System;
using System.Linq;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
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

        [Given(@"I have set the section location to ([^\s]+) (.*)")]
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


        [Given(@"I have created a section (.*) from the course and term")]
        public void GivenIHaveCreatedASectionFromTheCourseAndTerm(
            string sectionNumber)
        {
            var courseId = DomainHelper.GetId<Course>();
            var termId = DomainHelper.GetId<Term>();
            GivenIHaveCreatedASectionFromTheCourseAndTerm(
                sectionNumber, courseId, termId);
        }

        [Given(@"I have created a section (.*) from the course with term (.*)")]
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
            using (var ctx = new Ncqrs.Spec.EventContext())
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

        [Given(@"I have changed the section start date to (.*) and the end date to (.*)")]
        public void GivenIHaveChangedTheSectionDates(
            string startDateString,
            string endDateString)
        {
            var startDate = DateTime.Parse(startDateString);
            var endDate = DateTime.Parse(endDateString);

            DomainHelper.GivenEvent<Section>(
                new SectionDatesChangedEvent(
                    DomainHelper.GetId<Section>(),
                    startDate,
                    endDate));
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

        [When(@"I change the section credit type to (.*)")]
        public void WhenIChangeTheSectionCreditTypeTo(
            string creditTypeString)
        {
            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);

            var cmd = new ChangeSectionCreditTypeCommand()
                          {
                              SectionId = DomainHelper.GetId<Section>(),
                              CreditType = creditType
                          };

            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the start date to (.*) and the end date to (.*)")]
        public void WhenIChangeTheSectoinDates(
            string startDateString,
            string endDateString)
        {
            var start = DateTime.Parse(startDateString);
            var end = DateTime.Parse(endDateString);

            var cmd = new ChangeSectionDatesCommand()
                          {
                              SectionId = DomainHelper.GetId<Section>(),
                              StartDate = start,
                              EndDate = end
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the section number to (.*)")]
        public void WhenIChangeTheSectionNumberTo(
            string sectionNumber)
        {
            var cmd = new ChangeSectionNumberCommand()
                          {
                              SectionId = DomainHelper.GetId<Section>(),
                              SectionNumber = sectionNumber
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the section's CEUs to (.*)")]
        public void WhenIChangeTheSectionSCEUsTo(
            string ceusString)
        {
            var ceus = decimal.Parse(ceusString);
            var cmd = new ChangeSectionCEUsCommand()
                          {
                              SectionId = DomainHelper.GetId<Section>(),
                              CEUs = ceus
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the section's title to ""(.*)""")]
        public void WhenIChangeTheSectionSTitleTo(
            string title)
        {
            var cmd = new ChangeSectionTitleCommand()
                          {
                              SectionId = DomainHelper.GetId<Section>(),
                              NewTitle = title
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the section's term to (.*)")]
        public void WhenIChangeTheSectionSTermTo(
            string termAbbreviation)
        {
            var termId = DomainHelper.GetId<Term>(termAbbreviation);

            var cmd = new ChangeSectionTermCommand()
                          {
                              SectionId = DomainHelper.GetId<Section>(),
                              TermId = termId
                          };
            DomainHelper.WhenExecuting(cmd);
        }


        [Then(@"the section's term is (.*)")]
        public void ThenTheSectionSTermIs(
            string termAbbreviation)
        {
            var termId = DomainHelper.GetId<Term>(termAbbreviation);

            var e = (dynamic) DomainHelper.GetEvent<SectionCreatedEvent>()
                    ?? DomainHelper.GetEvent<SectionTermChangedEvent>();

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

        [Then(@"the section's start date is not set")]
        [Then(@"the section's end date is not set")]
        public void ThenTheSectionsDatesAreNotSet()
        {
            var datesChangedEvents = DomainHelper
                .GetEvents<SectionDatesChangedEvent>()
                .ToArray();
            Assert.That(datesChangedEvents, Is.Empty);
        }

        [Then(@"the section's start date is blank")]
        [Then(@"the section's end date is blank")]
        public void ThenTheSectionsDatesAreBlank()
        {
            var e = DomainHelper.GetEvent<SectionDatesRemovedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the section's start date is (\d.*)")]
        [Then(@"the section start date is (\d.*)")]
        public void ThenTheSectionSStartDateIs(
            string startDateString)
        {
            var startDate = DateTime.Parse(startDateString);

            var e = DomainHelper.GetEvent<SectionDatesChangedEvent>();
            Assert.That(e.Start, Is.EqualTo(startDate));
        }

        [Then(@"the section's end date is (\d.*)")]
        [Then(@"the section end date is (\d.*)")]
        public void ThenTheSectionSEndDateIs(
            string endDateString)
        {
            var endDate = DateTime.Parse(endDateString);

            var e = DomainHelper.GetEvent<SectionDatesChangedEvent>();
            Assert.That(e.End, Is.EqualTo(endDate));
        }

        [Then(@"the section's title is ""(.*)""")]
        public void ThenTheSectionSTitleIs(
            string sectionTitle)
        {
            var e = DomainHelper.GetEvent<SectionTitleChangedEvent>();
            Assert.That(e.Title, Is.EqualTo(sectionTitle));
        }

        [Then(@"the section's course type is (.*)")]
        public void ThenTheSectionSCourseTypeIs(
            string sectionCourseType)
        {
            var courseTypes = CourseTypeSteps.ParseCourseTypes(sectionCourseType);

            var e = DomainHelper.GetEvents<SectionCourseTypeAddedEvent>()
                .Last();

            Assert.That(e.CourseTypeAdded, Is.EqualTo(courseTypes.Single()));
        }


        [Then(@"the course type ([^\s]+) is added to the section")]
        public void ThenTheCourseTypeIsAddedToTheSection(
            string sectionCourseType)
        {
            var courseType = CourseTypeSteps.ParseCourseTypes(sectionCourseType).Single();

            var @event = DomainHelper
                .GetEvents<SectionCourseTypeAddedEvent>()
                .Where(e => e.CourseTypeAdded == courseType)
                .Single();

            Assert.That(@event.CourseTypeAdded, Is.EqualTo(courseType));
        }


        [Then(@"the course type ([^\s]+) is removed from the section")]
        public void ThenTheCourseTypeIsRemovedFromTheSection(
            string sectionCourseType)
        {
            var courseType = CourseTypeSteps.ParseCourseTypes(sectionCourseType).Single();

            var @event = DomainHelper
                .GetEvents<SectionCourseTypeRemovedEvent>()
                .Where(e => e.CourseTypeRemoved == courseType)
                .Single();

            Assert.That(@event.CourseTypeRemoved, Is.EqualTo(courseType));
        }


        [Then(@"the section course type should be (.*)")]
        [Then(@"the section's current course type is (.*)")]
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


        [Then(@"the section's approval number is (.*)")]
        public void ThenTheSectionSApprovalNumberIs(
            string approvalNumber)
        {
            var e = DomainHelper.GetEvent<SectionApprovalNumberChangedEvent>();
            Assert.That(e.ApprovalNumber, Is.EqualTo(approvalNumber));
        }

        [Then(@"the section's CIP is (.*)")]
        public void ThenTheSectionSCIPIs(
            string cip)
        {
            var e = DomainHelper.GetEvent<SectionCIPChangedEvent>();
            Assert.That(e.CIP, Is.EqualTo(cip));
        }

        [Then(@"the section location is (.*)")]
        public void ThenTheSectionLocationIs(
            string locationAbbreviation)
        {
            var locationId = DomainHelper.GetId<Location>(locationAbbreviation);

            var e = DomainHelper.GetEvent<SectionLocationChangedEvent>();
            Assert.That(e.LocationId, Is.EqualTo(locationId));
        }

        [Then(@"the section location abbreviation is ([^\s]+)")]
        public void ThenTheSectionLocationAbbreviationIs(
            string locationAbbreviation)
        {
            var e = DomainHelper.GetEvent<SectionLocationChangedEvent>();
            Assert.That(e.LocationAbbreviation, Is.EqualTo(locationAbbreviation));
        }

        [Then(@"the section location name is (.*)")]
        public void ThenTheSectionLocationNameIs(
            string locationName)
        {
            var e = DomainHelper.GetEvent<SectionLocationChangedEvent>();
            Assert.That(e.LocationName, Is.EqualTo(locationName));
        }


        [Then(@"the topic code is blank")]
        public void ThenTheTopicCodeIsBlank()
        {
            var e = DomainHelper.GetEvent<SectionTopicCodeRemovedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the section's topic code is ([A-Z0-9]+)")]
        public void ThenTheSectionSTopicCodeIs(
            string topicCodeAbbreviation)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>(topicCodeAbbreviation);

            var e = DomainHelper.GetEvent<SectionTopicCodeChangedEvent>();

            Assert.That(e, Is.Not.Null);
            Assert.That(e.TopicCodeId, Is.EqualTo(topicCodeId));
            Assert.That(e.TopicCodeAbbreviation, Is.EqualTo(topicCodeAbbreviation));
        }

        [Then(@"the topic code is ([A-Z0-9]+) (.*)")]
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

        [Then(@"the section is created")]
        public void ThenTheSectionIsCreated()
        {
            var e = DomainHelper.GetEvent<SectionCreatedEvent>();
            Assert.That(e, Is.Not.Null);
        }


        [Then(@"the section credit type should be (.*)")]
        [Then(@"the section's credit type is (.*)")]
        public void ThenTheSectionSCreditTypeIs(
            string creditTypeString)
        {
            var creditType = CourseCreditTypeSteps.ParseCreditType(creditTypeString);

            var e = DomainHelper.GetEvent<SectionCreditTypeChangedEvent>();
            Assert.That(e.CreditType, Is.EqualTo(creditType));
        }

        [Then(@"the section's status is pending")]
        public void ThenTheSectionSStatusIsPending()
        {
            var e = DomainHelper.GetEvent<SectionMadePendingEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the location is blank")]
        public void ThenTheLocationIsBlank()
        {
            var events = DomainHelper
                .GetEvents<SectionLocationChangedEvent>()
                .ToArray();

            Assert.That(events, Is.Empty);
        }

        [Then(@"the section's CEUs is ([^\s]+)")]
        [Then(@"the section's CEUs are ([^\s]+)")]
        public void ThenTheSectionSCEUsIs(
            string ceusString)
        {
            var ceus = decimal.Parse(ceusString);
            var e = DomainHelper.GetEvent<SectionCEUsChangedEvent>();
            Assert.That(e.CEUs, Is.EqualTo(ceus));
        }

        [Then(@"the section number is (.*)")]
        public void ThenTheSectionNumberIs(
            string sectionNumber)
        {
            var e = (dynamic) DomainHelper.GetEvent<SectionCreatedEvent>()
                    ?? DomainHelper.GetEvent<SectionNumberChangedEvent>();

            Assert.That(e.SectionNumber, Is.EqualTo(sectionNumber));
        }


    }
}
