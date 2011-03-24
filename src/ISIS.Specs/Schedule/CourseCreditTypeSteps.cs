using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseCreditTypeSteps
    {

        [When(@"I change the credit type to (.*)")]
        public void WhenIChangeTheCreditTypeTo(
            string creditTypeString)
        {
            ScenarioContext.Current.Pending();
        }


        [Then(@"the credit type is (.*)")]
        public void ThenTheCreditTypeIs(string creditTypeString)
        {
            ScenarioContext.Current.Pending();
        }
    
    }
}
