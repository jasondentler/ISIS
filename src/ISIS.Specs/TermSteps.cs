using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ISIS.Schedule;
using TechTalk.SpecFlow;

namespace ISIS
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

            DomainHelper.GivenEvent(id, new TermCreatedEvent(id, name, abbreviation, start, end));
        }


    }
}
