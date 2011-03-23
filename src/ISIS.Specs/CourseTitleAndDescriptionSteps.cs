using ISIS.Schedule;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Specs
{
    [Binding]
    public class CourseTitleAndDescriptionSteps
    {

        [Given(@"the course long title is (.*)")]
        public void GivenTheCourseLongTitleIs(string longTitle)
        {
            DomainHelper.GivenEvent(new CourseLongTitleChangedEvent(
                                        DomainHelper.GetEventSourceId(),
                                        longTitle));
        }

        [Given(@"I have changed the course description to (.*)")]
        public void GivenIHaveChangedTheCourseDescriptionTo(string description)
        {
            DomainHelper.GivenEvent(new CourseDescriptionChangedEvent(
                                        DomainHelper.GetEventSourceId(),
                                        description));
        }

        [Given(@"I have set the course long title to (.*)")]
        public void GivenIHaveSetTheCourseLongTitleTo(string longTitle)
        {
            DomainHelper.GivenEvent(new CourseLongTitleChangedEvent(
                                        DomainHelper.GetEventSourceId(),
                                        longTitle));
        }


        [When(@"I change the course title to (.*)")]
        public void WhenIChangeTheCourseTitleTo(string title)
        {
            var cmd = new ChangeCourseTitleCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId(),
                              NewTitle = title
                          };
            DomainHelper.WhenExecuting(cmd);
        }


        [When(@"I change the course long title to (.*)")]
        public void WhenIChangeTheCourseLongTitleTo(string longTitle)
        {
            var cmd = new ChangeCourseLongTitleCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId(),
                              NewLongTitle = longTitle
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [When(@"I change the course description to (.*)")]
        public void WhenIChangeTheCourseDescriptionTo(string description)
        {
            var cmd = new ChangeCourseDescriptionCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId(),
                              NewDescription = description
                          };
            DomainHelper.WhenExecuting(cmd);
        }


        [Then(@"the course title is (.*)")]
        public void ThenTheCourseTitleShouldBe(string title)
        {
            var e = DomainHelper.GetEvent<CourseTitleChangedEvent>();
            Assert.That(e.Title, Is.EqualTo(title));
        }

        [Then(@"the course long title is (.*)")]
        public void ThenTheCourseLongTitleShouldBe(string longTitle)
        {
            var e = DomainHelper.GetEvent<CourseLongTitleChangedEvent>();
            Assert.That(e.LongTitle, Is.EqualTo(longTitle));
        }

        [Then(@"the course description is (.*)")]
        public void ThenTheCourseDescriptionIs(string description)
        {
            var e = DomainHelper.GetEvent<CourseDescriptionChangedEvent>();
            Assert.That(e.Description, Is.EqualTo(description));
        }


    }
}
