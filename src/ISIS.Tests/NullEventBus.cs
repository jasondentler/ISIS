using System.Collections.Generic;
using Ncqrs.Eventing;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class NullEventBus : IEventBus 
    {

        public void Publish(IEvent eventMessage)
        {
        }

        public void Publish(IEnumerable<IEvent> eventMessages)
        {
        }

    }
}
