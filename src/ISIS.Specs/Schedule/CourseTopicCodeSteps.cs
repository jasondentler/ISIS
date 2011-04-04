using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class CourseTopicCodeSteps
    {
        
        [Given(@"I have created a topic code ([A-Z]+) (.*)")]
        public void GivenIHaveCreatedATopicCode(
            string abbreviation,
            string description)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<TopicCode>(id, abbreviation);

            DomainHelper.GivenEvent(id, new TopicCodeCreatedEvent(
                id, 
                abbreviation, 
                description));
        }

        [Given(@"I have changed the course's topic code to ([A-Z]+) (.*)")]
        public void GivenIHaveChangedTheCourseSTopicCodeTo(
            string topicCodeAbbreviation,
            string topicCodeDescription)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>(topicCodeAbbreviation);

            DomainHelper.GivenEvent<Course>(
                new CourseTopicCodeChangedEvent(
                    DomainHelper.GetEventSourceId<Course>(),
                    new TopicCodeMemento(
                        topicCodeId,
                        topicCodeAbbreviation,
                        topicCodeDescription)));
        }

        [When(@"I create a topic code ([A-Z]+) (.*)")]
        public void WhenICreateATopicCode(
            string abbreviation,
            string description)
        {
            var cmd = new CreateTopicCodeCommand()
                          {
                              Abbreviation = abbreviation,
                              Description = description
                          };
            DomainHelper.WhenExecuting(cmd);

            var @event = DomainHelper.GetEvents()
                .Select(e => e.Payload)
                .Where(e => e is TopicCodeCreatedEvent)
                .Cast<TopicCodeCreatedEvent>()
                .Single();

            DomainHelper.SetId<TopicCode>(@event.TopicCodeId, abbreviation);
        }

        [When(@"I change the topic code abbreviation to (.*)")]
        public void WhenIChangeTheTopicCodeAbbreviation(
            string newAbbreviation)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>();

            var cmd = new ChangeTopicCodeAbbreviationCommand()
                          {
                              TopicCodeId = topicCodeId,
                              Abbreviation = newAbbreviation
                          };
            DomainHelper.WhenExecuting(cmd);

            DomainHelper.SetId<TopicCode>(topicCodeId, newAbbreviation);
        }

        [When(@"I change the topic code description to (.*)")]
        public void WhenIChangeTheTopicCodeDescriptionTo(
            string topicCodeDescription)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>();

            var cmd = new ChangeTopicCodeDescriptionCommand()
                          {
                              TopicCodeId = topicCodeId,
                              Description = topicCodeDescription
                          };
            DomainHelper.WhenExecuting(cmd);
        }
        
        [When(@"I change the courses's topic code to ([A-Z]+)")]
        public void WhenIChangeTheCoursesSTopicCodeTo(string topicCodeAbbreviation)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>(topicCodeAbbreviation);

            var cmd = new ChangeCourseTopicCodeCommand()
                          {
                              CourseId = DomainHelper.GetEventSourceId<Course>(),
                              TopicCodeId = topicCodeId
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [Then(@"the topic code ([A-Z]+) is created")]
        public void ThenTheTopicCodeIsCreated(
            string abbreviation)
        {
            var e = DomainHelper.GetEvent<TopicCodeCreatedEvent>();
            Assert.That(e.Abbreviation, Is.EqualTo(abbreviation));
        }

        [Then(@"the topic code abbreviation is (.*)")]
        public void ThenTheTopicCodeAbbreviationIs(
            string abbreviation)
        {
            var e = (dynamic) DomainHelper.GetEvent<TopicCodeCreatedEvent>()
                    ?? DomainHelper.GetEvent<TopicCodeAbbreviationChangedEvent>();
            Assert.That(e.Abbreviation, Is.EqualTo(abbreviation));
        }

        [Then(@"the previous topic code abbreviation is (.*)")]
        public void ThenThePreviousTopicCodeAbbreviationIs(string abbreviation)
        {
            var e = DomainHelper.GetEvent<TopicCodeAbbreviationChangedEvent>();
            Assert.That(e.PreviousAbbreviation, Is.EqualTo(abbreviation));
        }

        [Then(@"the topic code description is (.*)")]
        public void ThenTheTopicCodeDescriptionIs(
            string description)
        {
            var e = (dynamic) DomainHelper.GetEvent<TopicCodeCreatedEvent>()
                    ?? DomainHelper.GetEvent<TopicCodeDescriptionChangedEvent>();
            Assert.That(e.Description, Is.EqualTo(description));
        }

        [Then(@"the previous topic code description is (.*)")]
        public void ThenThePreviousTopicCodeDescriptionIs(
            string description)
        {
            var e = DomainHelper.GetEvent<TopicCodeDescriptionChangedEvent>();
            Assert.That(e.PreviousDescription, Is.EqualTo(description));
        }


        [Then(@"the course's topic code is (.*)")]
        public void ThenTheCourseSTopicCodeIs(string topicCodeAbbreviation)
        {
            var topicCodeId = DomainHelper.GetId<TopicCode>(topicCodeAbbreviation);

            var e = DomainHelper.GetEvent<CourseTopicCodeChangedEvent>();
            Assert.That(e.TopicCodeId, Is.EqualTo(topicCodeId));
            Assert.That(e.TopicCodeAbbreviation, Is.EqualTo(topicCodeAbbreviation));
        }




    }
}
