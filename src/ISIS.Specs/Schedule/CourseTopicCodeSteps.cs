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

        private Dictionary<string, Guid> GetMap()
        {
            var ctx = ScenarioContext.Current;
            if (!ctx.ContainsKey("topicCodeMap"))
            {
                var map = new Dictionary<string, Guid>();
                ctx["topicCodeMap"] = map;
                return map;
            }
            return ctx.Get<Dictionary<string, Guid>>("topicCodeMap");
        }

        private void AddTopicCode(Guid id, string abbreviation)
        {
            GetMap().Add(abbreviation, id);
        }

        private void RemoveTopicCode(string abbreviation)
        {
            GetMap().Remove(abbreviation);
        }

        private void RemoveTopicCode(Guid id)
        {
            GetMap().Remove(GetTopicCodeAbbreviation(id));
        }

        private string GetTopicCodeAbbreviation(Guid id)
        {
            return GetMap()
                .Where(item => item.Value == id)
                .Select(item => item.Key)
                .Single();
        }

        private Guid GetTopicCodeId(string abbreviation)
        {
            return GetMap()[abbreviation];
        }

        private Guid GetTheTopicCodeId()
        {
            return GetMap().Single().Value;
        }

        [Given(@"I have created a topic code ([A-Z]+) (.*)")]
        public void GivenIHaveCreatedATopicCode(
            string abbreviation,
            string description)
        {
            var id = Guid.NewGuid();
            AddTopicCode(id, abbreviation);
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
            var topicCodeId = GetTopicCodeId(topicCodeAbbreviation);
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

            AddTopicCode(@event.TopicCodeId, @event.Abbreviation);
        }

        [When(@"I change the topic code abbreviation to (.*)")]
        public void WhenIChangeTheTopicCodeAbbreviation(
            string newAbbreviation)
        {
            var topicCodeId = GetTheTopicCodeId();

            var cmd = new ChangeTopicCodeAbbreviationCommand()
                          {
                              TopicCodeId = topicCodeId,
                              Abbreviation = newAbbreviation
                          };
            DomainHelper.WhenExecuting(cmd);

            RemoveTopicCode(topicCodeId);
            AddTopicCode(topicCodeId, newAbbreviation);
        }

        [When(@"I change the topic code description to (.*)")]
        public void WhenIChangeTheTopicCodeDescriptionTo(
            string topicCodeDescription)
        {
            var topicCodeId = GetTheTopicCodeId();
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
            var topicCodeId = GetTopicCodeId(topicCodeAbbreviation);
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
            var topicCodeId = GetTopicCodeId(topicCodeAbbreviation);
            var e = DomainHelper.GetEvent<CourseTopicCodeChangedEvent>();
            Assert.That(e.TopicCodeId, Is.EqualTo(topicCodeId));
            Assert.That(e.TopicCodeAbbreviation, Is.EqualTo(topicCodeAbbreviation));
        }




    }
}
