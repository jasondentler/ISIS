using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace ISIS.DomainTests.Environment
{
    public class TestEventStoreWrapper : IEventStore
    {
        private readonly Func<IEventStore> _eventStoreConstructor;
        private IEventStore _wrappedEventStore;

        public TestEventStoreWrapper(Func<IEventStore> eventStoreConstructor)
        {
            _eventStoreConstructor = eventStoreConstructor;
            _wrappedEventStore = eventStoreConstructor();
        }

        public IEnumerable<ISourcedEvent> GetAllEvents(Guid id)
        {
            return _wrappedEventStore.GetAllEvents(id);
        }

        public IEnumerable<ISourcedEvent> GetAllEventsSinceVersion(Guid id, long version)
        {
            return _wrappedEventStore.GetAllEventsSinceVersion(id, version);
        }

        public void Save(IEventSource source)
        {
            _wrappedEventStore.Save(source);
        }

        //private void Save(IEnumerable<ISourcedEvent> events)
        //{
        //    bool claim = events.Any(e => e.EventSequence != 0);
        //    foreach (var @event in events)
        //    {
        //        if (claim)
        //            @event.ClaimEvent(_eventSourceId, _events.Count + 1);
        //        _events.Enqueue(@event);
        //    }
        //}

        public void Setup(Guid eventSourceId, IEnumerable<ISourcedEvent> events)
        {
            Save(new FakeEventSource(eventSourceId, events));
        }

        public void Clear()
        {
            _wrappedEventStore = _eventStoreConstructor();
        }

    }
}
