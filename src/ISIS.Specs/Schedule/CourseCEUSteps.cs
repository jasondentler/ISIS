using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            ScenarioContext.Current.Pending();
        }


        [When(@"I change the CEUs to (.+)")]
        [When(@"I change CEUs to (.+)")]
        public void WhenIChangeTheCEUsTo(
            string ceusString)
        {
            ScenarioContext.Current.Pending();
        }


        [Then(@"the CEUs are (.+)")]
        public void ThenTheCEUsAre(string ceusString)
        {
            ScenarioContext.Current.Pending();
        }

    }
}
