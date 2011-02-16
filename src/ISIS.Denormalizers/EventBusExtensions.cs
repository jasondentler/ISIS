using System.Reflection;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public static class EventBusExtensions 
    {

        public static IEventBus RegisterDenormalizers(this InProcessEventBus eventBus)
        {
            return eventBus.RegisterDenormalizers(Assembly.GetExecutingAssembly());
        }

        public static IEventBus RegisterDenormalizers(this InProcessEventBus eventBus, Assembly assembly)
        {
            eventBus.RegisterAllHandlersInAssembly(assembly);
            return eventBus;
        }

    }
}
