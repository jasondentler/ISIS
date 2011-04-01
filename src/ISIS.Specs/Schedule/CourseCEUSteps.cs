using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseCEUSteps
    {

        [Given(@"I have changed the CEUs to (.+)")]
        public void GivenIHaveChangedTheCEUsTo(
            string ceusString)
        {
            var ceus = decimal.Parse(ceusString);
            DomainHelper.GivenEvent<Course>(new CourseCEUsChangedEvent(
                                        DomainHelper.GetEventSourceId<Course>(),
                                        ceus));
        }


        [When(@"I change the CEUs to (.+)")]
        [When(@"I change CEUs to (.+)")]
        public void WhenIChangeTheCEUsTo(
            string ceusString)
        {
            var ceus = decimal.Parse(ceusString);

            var cmd = new ChangeCourseCEUsCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId<Course>(),
                              CEUs = ceus
                          };
            DomainHelper.WhenExecuting(cmd);
        }


        [Then(@"the CEUs are (.+)")]
        public void ThenTheCEUsAre(string ceusString)
        {
            var ceus = decimal.Parse(ceusString);
            var e = DomainHelper.GetEvent<CourseCEUsChangedEvent>();
            Assert.That(e.Ceus, Is.EqualTo(ceus));
        }

    }
}
