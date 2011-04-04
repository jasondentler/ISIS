using System;
using TechTalk.SpecFlow;

namespace ISIS.Schedule
{
    [Binding]
    public class LocationSteps
    {

        [Given(@"I have created a location ([^\s]+) (.*)")]
        [Given(@"I have created the location ([^\s]+) (.*)")]
        public void GivenIHaveCreatedALocation(
            string abbreviation,
            string name)
        {
            var id = Guid.NewGuid();
            DomainHelper.SetId<Location>(id, abbreviation);

            DomainHelper.GivenEvent(
                id,
                new LocationCreatedEvent(id, abbreviation, name));
        }


    }
}
