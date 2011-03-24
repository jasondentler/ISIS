using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Eventing.Storage;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseTypeSteps
    {

        public static IEnumerable<CourseTypes> ParseCourseTypes(string courseTypeString)
        {
            return courseTypeString
                .Split(' ')
                .Select(s => s.Trim())
                .Where(s => !String.IsNullOrWhiteSpace(s))
                .Select(s => Enum.Parse(typeof (CourseTypes), s))
                .Cast<CourseTypes>();
        }

        private static IEnumerable<CourseTypes> GetCurrentCourseType()
        {
            var store = NcqrsEnvironment.Get<IEventStore>();
            var stream = store.ReadFrom(DomainHelper.GetEventSourceId(), 0, long.MaxValue);
            var mostRecentCourseTypeEvent = stream
                .Select(e => e.Payload)
                .Where(e => e is CourseTypeAddedToCourseEvent || e is CourseTypeRemovedFromCourseEvent)
                .Select(e => (dynamic) e)
                .LastOrDefault();

            return mostRecentCourseTypeEvent == null
                       ? new CourseTypes[0]
                       : mostRecentCourseTypeEvent.CurrentTypes;
        }

        [Given(@"I have added the (.*) course type")]
        public void GivenIHaveAddedTheCourseType(string courseTypeString)
        {
            var courseTypeToAdd = ParseCourseTypes(courseTypeString).Single();
            var existingCourseTypes = GetCurrentCourseType();
            var newCourseTypes = existingCourseTypes
                .Union(new[] {courseTypeToAdd})
                .Distinct()
                .OrderBy(ce => ce)
                .ToArray();

            DomainHelper.GivenEvent(new CourseTypeAddedToCourseEvent(
                                        DomainHelper.GetEventSourceId(),
                                        courseTypeToAdd,
                                        newCourseTypes
                                        ));
        }


        [When(@"I add the (.*) course type")]
        public void WhenIAddTheCourseType(string courseTypeString)
        {
            var courseTypeToAdd = ParseCourseTypes(courseTypeString).Single();
            var cmd = new AddCourseTypeToCourseCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId(),
                              Type = courseTypeToAdd
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I remove the (.*) course type")]
        public void WhenIRemoveTheCourseType(string courseTypeString)
        {
            var courseTypeToRemove = ParseCourseTypes(courseTypeString).Single();
            var cmd = new RemoveCourseTypeFromCourse()
            {
                CourseId = DomainHelper.GetEventSourceId(),
                Type = courseTypeToRemove
            };
            DomainHelper.WhenExecuting(cmd);
        }

        [Then(@"the course type is (.*)")]
        public void ThenTheCourseTypeIs(string courseTypes)
        {
            var expected = ParseCourseTypes(courseTypes).ToArray();
            var actual = GetCurrentCourseType();
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Then(@"the (.*) course type is added")]
        public void ThenTheCourseTypeIsAdded(string courseTypeString)
        {
            var courseType = ParseCourseTypes(courseTypeString).Single();
            var e = DomainHelper.GetEvent<CourseTypeAddedToCourseEvent>();
            Assert.That(e.TypeAdded, Is.EqualTo(courseType));
        }

        [Then(@"the (.*) course type is removed")]
        public void ThenTheCourseTypeIsRemoved(string courseTypeString)
        {
            var courseType = ParseCourseTypes(courseTypeString).Single();
            var e = DomainHelper.GetEvent<CourseTypeRemovedFromCourseEvent>();
            Assert.That(e.TypeRemoved, Is.EqualTo(courseType));
        }


        [Then(@"the current course type is (.*)")]
        public void ThenTheCurrentCourseTypeIs(string courseTypeString)
        {
            var courseTypes = ParseCourseTypes(courseTypeString);
            AssertCurrentCourseTypeIs(courseTypes);
        }

        [Then(@"the current course types are (.*) and (.*)")]
        public void ThenTheCurrentCourseTypesAre(string courseTypeString1, string courseTypeString2)
        {
            var courseType1 = ParseCourseTypes(courseTypeString1).Single();
            var courseType2 = ParseCourseTypes(courseTypeString2).Single();
            AssertCurrentCourseTypeIs(new[] {courseType1, courseType2});
        }

        private void AssertCurrentCourseTypeIs(IEnumerable<CourseTypes> courseTypes)
        {
            var actual = GetCurrentCourseType();
            var expected = courseTypes.Distinct().OrderBy(ct => ct);
            Assert.That(actual, Is.EqualTo(expected));
        }


    }
}
