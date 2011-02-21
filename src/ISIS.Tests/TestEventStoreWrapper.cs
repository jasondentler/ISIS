using System;
using System.Collections.Generic;
using Ncqrs.Eventing;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace ISIS
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

        public CommittedEventStream ReadFrom(Guid id, long minVersion, long maxVersion)
        {
            return _wrappedEventStore.ReadFrom(id, minVersion, maxVersion);
        }

        public void Store(UncommittedEventStream eventStream)
        {
            _wrappedEventStore.Store(eventStream);
        }

        public void Setup(Guid eventSourceId, IEnumerable<UncommittedEvent> events)
        {
            var strm = new UncommittedEventStream(Guid.NewGuid());
            foreach (var evnt in events)
                strm.Append(evnt);
            Store(strm);
        }

        public void Clear()
        {
            _wrappedEventStore = _eventStoreConstructor();
        }

    }
}
