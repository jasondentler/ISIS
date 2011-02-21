using System.Collections.Generic;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class NullEventBus : IEventBus 
    {

        public void Publish(IPublishableEvent eventMessage)
        {
        }

        public void Publish(IEnumerable<IPublishableEvent> eventMessages)
        {
        }

    }
}
