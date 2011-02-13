using Ncqrs.Eventing.Storage;

namespace ISIS.DomainTests.Environment
{
    public class SimpleTestEventStoreWrapper<T> : TestEventStoreWrapper
        where T: IEventStore, new()
    {
        
        public SimpleTestEventStoreWrapper()
            : base(() => new T())
        {
        }

    }
}