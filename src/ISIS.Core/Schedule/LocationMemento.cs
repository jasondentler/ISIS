using System;

namespace ISIS.Schedule
{
    public struct LocationMemento
    {
        private readonly Guid _locationId;
        private readonly string _abbreviation;
        private readonly string _name;

        public LocationMemento(
            Guid locationId,
            string abbreviation,
            string name)
        {
            _locationId = locationId;
            _abbreviation = abbreviation;
            _name = name;
        }

        public Guid LocationId { get { return _locationId; } }
        public string Abbreviation { get { return _abbreviation; } }
        public string Name { get { return _name; } }
    }
}
