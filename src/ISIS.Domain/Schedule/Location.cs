using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class Location : AggregateRootMappedByConvention
    {
        private string _abbreviation;
        private string _name;

        [Inject]
        private Location()
        {
        }

        protected void OnCreated(LocationCreatedEvent @event)
        {
            _abbreviation = @event.Abbreviation;
            _name = @event.Name;
        }

        public LocationMemento BuildMemento()
        {
            return new LocationMemento(
                EventSourceId,
                _abbreviation,
                _name);
        }
    }
}
