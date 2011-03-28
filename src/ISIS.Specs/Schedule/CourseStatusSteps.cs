﻿using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseStatusSteps
    {

        [Given(@"I have deactivated the course")]
        public void GivenIHaveDeactivatedTheCourse()
        {
            DomainHelper.GivenEvent(
                new CourseDeactivatedEvent(
                    DomainHelper.GetEventSourceId()));
        }

        [Given(@"I have made the course obsolete")]
        public void GivenIHaveMadeTheCourseObsolete()
        {
            DomainHelper.GivenEvent(
                new CourseMadeObsoleteEvent(
                    DomainHelper.GetEventSourceId()));
        }

        [Given(@"I have made the course pending")]
        public void GivenIHaveMadeTheCoursePending()
        {
            DomainHelper.GivenEvent(
                new CourseMadePendingEvent(
                    DomainHelper.GetEventSourceId()));
        }

        [When(@"I activate the course")]
        public void WhenIActivateTheCourse()
        {
            var cmd = new ActivateCourseCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId()
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I deactivate the course")]
        public void WhenIDeactivateTheCourse()
        {
            var cmd = new DeactivateCourseCommand()
            {
                CourseId = DomainHelper.GetEventSourceId()
            };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I make the course pending")]
        public void WhenIMakeTheCoursePending()
        {
            var cmd = new MakeCoursePendingCommand()
            {
                CourseId = DomainHelper.GetEventSourceId()
            };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I make the course obsolete")]
        public void WhenIMakeTheCourseObsolete()
        {
            var cmd = new MakeCourseObsoleteCommand()
            {
                CourseId = DomainHelper.GetEventSourceId()
            };
            DomainHelper.WhenExecuting(cmd);
        }

        [Then(@"the course is active")]
        public void ThenTheCourseShouldBeActive()
        {
            var e = DomainHelper.GetEvent<CourseActivatedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the course is inactive")]
        public void ThenTheCourseIsInactive()
        {
            var e = DomainHelper.GetEvent<CourseDeactivatedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the course is pending")]
        public void ThenTheCourseIsPending()
        {
            var e = DomainHelper.GetEvent<CourseMadePendingEvent>();
            Assert.That(e, Is.Not.Null);
        }
        
        [Then(@"the course is obsolete")]
        public void ThenTheCourseIsObsolete()
        {
            var e = DomainHelper.GetEvent<CourseMadeObsoleteEvent>();
            Assert.That(e, Is.Not.Null);
        }




    }
}
