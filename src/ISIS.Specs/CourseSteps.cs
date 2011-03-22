using System;
using System.Collections.Generic;
using System.Linq;
using ISIS.Schedule;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Specs
{
    [Binding]
    public class CourseSteps 
    {
        
        [BeforeScenario("domain")]
        public void Setup()
        {
            Configuration.Configure();
        }

        [When(@"I create an (.*) course ([A-Z]{4}) (\d{4}) (.*)")]
        public void WhenICreateAnCourse(
            string courseTypeString,
            string rubric,
            string number,
            string title)
        {
            WhenICreateACourse(courseTypeString, rubric, number, title);
        }

        [When(@"I create a (.*) course ([A-Z]{4}) (\d{4}) (.*)")]
        public void WhenICreateACourse(
            string courseTypeString,
            string rubric,
            string number,
            string title)
        {
            var courseTypes = ParseCourseTypes(courseTypeString);
            var cmd = new CreateCreditCourseCommand()
                          {
                              CourseId = Guid.NewGuid(),
                              Rubric = rubric,
                              CourseNumber = number,
                              Title = title,
                              Types = courseTypes
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        private static IEnumerable<CourseTypes> ParseCourseTypes(string courseTypeString)
        {
            return courseTypeString
                .Split(' ')
                .Select(s => s.Trim())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .Select(s => Enum.Parse(typeof (CourseTypes), s))
                .Cast<CourseTypes>();
        }

        [Then(@"the course should be created")]
        public void ThenTheCourseShouldBeCreated()
        {
            var cmd = DomainHelper.GetCommand<CreateCreditCourseCommand>();
            var e = DomainHelper.GetEvent<CreditCourseCreatedEvent>();
            Assert.That(e.CourseId, Is.EqualTo(cmd.CourseId));
        }

        [Then(@"the course rubric should be (.*)")]
        public void ThenTheCourseRubricShouldBe(string rubric)
        {
            var e = DomainHelper.GetEvent<CreditCourseCreatedEvent>();
            Assert.That(e.Rubric, Is.EqualTo(rubric));
        }

        [Then(@"the course number should be (.*)")]
        public void ThenTheCourseNumberShouldBe(string number)
        {
            var e = DomainHelper.GetEvent<CreditCourseCreatedEvent>();
            Assert.That(e.Number, Is.EqualTo(number));
        }

        [Then(@"the course title should be (.*)")]
        public void ThenTheCourseTitleShouldBe(string title)
        {
            var e = DomainHelper.GetEvent<CourseTitleChangedEvent>();
            Assert.That(e.Title, Is.EqualTo(title));
        }

        [Then(@"the course long title should be (.*)")]
        public void ThenTheCourseLongTitleShouldBe(string longTitle)
        {
            var e = DomainHelper.GetEvent<CourseLongTitleChangedEvent>();
            Assert.That(e.LongTitle, Is.EqualTo(longTitle));
        }

        [Then(@"the course should be active")]
        public void ThenTheCourseShouldBeActive()
        {
            var e = DomainHelper.GetEvent<CourseActivatedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the course type should be (.*)")]
        public void ThenTheCourseTypeShouldBe(string courseTypes)
        {
            var expected = ParseCourseTypes(courseTypes);
            var events = DomainHelper.GetEvents<CourseTypeAddedToCourseEvent>();
            var actual = events.Select(e => e.TypeAdded);
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Then(@"it should do nothing else")]
        public void ThenItShouldDoNothingElse()
        {
            Assert.That(DomainHelper.AllEventsWereTested(), Is.True);
        }


    }
}
