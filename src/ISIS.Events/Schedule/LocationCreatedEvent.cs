using System;

namespace ISIS.Schedule
{

    public class LocationCreatedEvent : IEvent
    {

        public Guid LocationId { get; private set; }
        public string Abbreviation { get; private set; }
        public string Name { get; private set; }

        public LocationCreatedEvent(
            Guid locationId,
            string abbreviation,
            string name)
        {
            LocationId = locationId;
            Abbreviation = abbreviation;
            Name = name;
        }
    }

}
