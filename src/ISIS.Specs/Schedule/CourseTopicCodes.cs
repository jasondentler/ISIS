using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseTopicCodes
    {

        [Given(@"I have created a topic code ([A-Z]+) (.*)")]
        public void GivenIHaveCreatedATopicCode(
            string abbreviation,
            string description)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have changed the course's topic code to ([A-Z]+)")]
        public void GivenIHaveChangedTheCourseSTopicCodeTo(string topicCodeAbbreviation)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I create a topic code ([A-Z]+) (.*)")]
        public void WhenICreateATopicCode(
            string abbreviation,
            string description)
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I change the (.*) topic code description to (.*)")]
        public void WhenIChangeTheTopicCodeDescriptionTo(
            string topicCodeAbbreviation,
            string topicCodeDescription)
        {
            ScenarioContext.Current.Pending();
        }


        [When(@"I change the courses's topic code to ([A-Z]+)")]
        public void WhenIChangeTheCoursesSTopicCodeTo(string topicCodeAbbreviation)
        {
            ScenarioContext.Current.Pending();
        }



        [Then(@"the topic code ([A-Z]+) is created")]
        public void ThenTheTopicCodeIsCreated(
            string abbreviation)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the topic code description is (.*)")]
        public void ThenTheTopicCodeDescriptionIs(
            string description)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the course's topic code is (.*)")]
        public void ThenTheCourseSTopicCodeIs(string topicCodeAbbreviation)
        {
            ScenarioContext.Current.Pending();
        }


    }
}
