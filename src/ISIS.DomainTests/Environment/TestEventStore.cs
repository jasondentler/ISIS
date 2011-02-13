using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace ISIS.DomainTests.Environment
{
    public class TestEventStore : IEventStore
    {
        private readonly IEventStore _wrappedEventStore;

        private Guid _eventSourceId;
        private Queue<ISourcedEvent> _events;

        public TestEventStore()
            : this(new InMemoryEventStore())
        {
        }

        public TestEventStore(IEventStore wrappedEventStore)
        {
            _wrappedEventStore = wrappedEventStore;
        }

        public IEnumerable<ISourcedEvent> GetAllEvents(Guid id)
        {
            return id != _eventSourceId
                       ? _wrappedEventStore.GetAllEvents(id)
                       : _events.ToArray();
        }

        public IEnumerable<ISourcedEvent> GetAllEventsSinceVersion(Guid id, long version)
        {
            return id != _eventSourceId
                       ? _wrappedEventStore.GetAllEventsSinceVersion(id, version)
                       : _events.Where(e => e.EventSequence > version);
        }

        public void Save(IEventSource source)
        {
            if (source.EventSourceId != _eventSourceId)
            {
                _wrappedEventStore.Save(source);
                return;
            }

            Save(source.GetUncommittedEvents());
        }

        private void Save(IEnumerable<ISourcedEvent> events)
        {
            bool claim = events.Any(e => e.EventSequence != 0);
            foreach (var @event in events)
            {
                if (claim)
                    @event.ClaimEvent(_eventSourceId, _events.Count + 1);
                _events.Enqueue(@event);
            }
        }

        public void Setup(Guid eventSourceId, IEnumerable<ISourcedEvent> events)
        {
            _eventSourceId = eventSourceId;
            _events = new Queue<ISourcedEvent>();
            Save(events);
        }

    }
}
