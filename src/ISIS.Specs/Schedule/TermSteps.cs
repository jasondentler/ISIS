using System;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class TermSteps
    {

        [Given(@"I have created a term ([^\s]+) (.*) from ([^\s]+) to ([^\s]+)")]
        public void GivenIHaveCreatedATerm(
            string abbreviation,
            string name,
            string startString,
            string endString)
        {
            var start = DateTime.Parse(startString);
            var end = DateTime.Parse(endString);

            var id = Guid.NewGuid();
            DomainHelper.SetId<Term>(id, abbreviation);

            DomainHelper.GivenEvent(
                id,
                new TermCreatedEvent(
                    id, abbreviation, name, start, end));
        }

        [When(@"I create the term ([^\s]+) (.*) from (.*) to (.*)")]
        public void WhenICreateTheTerm(
            string abbreviation,
            string name,
            string startDateString,
            String endDateString)
        {
            var startDate = DateTime.Parse(startDateString);
            var endDate = DateTime.Parse(endDateString);

            var id = Guid.NewGuid();
            DomainHelper.SetId<Term>(id, abbreviation);

            var cmd = new CreateTermCommand()
                          {
                              TermId = id,
                              Abbreviation = abbreviation,
                              Name = name,
                              StartDate = startDate,
                              EndDate = endDate
                          };
            DomainHelper.WhenExecuting(cmd);
        }

        [Then(@"the term is created")]
        public void ThenTheTermIsCreated()
        {
            var e = DomainHelper.GetEvent<TermCreatedEvent>();
            Assert.That(e, Is.Not.Null);
        }

        [Then(@"the term abbreviation is ([^\s]+)")]
        public void ThenTheTermAbbreviationIs(
            string abbreviation)
        {
            var e = DomainHelper.GetEvent<TermCreatedEvent>();
            Assert.That(e.Abbreviation, Is.EqualTo(abbreviation));
        }

        [Then(@"the term name is (.*)")]
        public void ThenTheTermNameIs(
            string name)
        {
            var e = DomainHelper.GetEvent<TermCreatedEvent>();
            Assert.That(e.Name, Is.EqualTo(name));
        }

        [Then(@"the term start date is (.*)")]
        public void ThenTheTermStartDateIs(
            string startDateString)
        {
            var startDate = DateTime.Parse(startDateString);
            var e = DomainHelper.GetEvent<TermCreatedEvent>();
            Assert.That(e.Start, Is.EqualTo(startDate));
        }

        [Then(@"the term end date is (.*)")]
        public void ThenTheTermEndDateIs(
            string endDateString)
        {
            var endDate = DateTime.Parse(endDateString);
            var e = DomainHelper.GetEvent<TermCreatedEvent>();
            Assert.That(e.End, Is.EqualTo(endDate));
        }


    }
}
