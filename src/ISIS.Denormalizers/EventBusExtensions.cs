using System.Reflection;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ninject;

namespace ISIS
{
    public static class EventBusExtensions 
    {

        public static IEventBus RegisterDenormalizers(this InProcessEventBus eventBus,IKernel kernel )
        {
            return eventBus.RegisterDenormalizers(kernel, Assembly.GetExecutingAssembly());
        }

        public static IEventBus RegisterDenormalizers(this InProcessEventBus eventBus, IKernel kernel, Assembly assembly)
        {
            eventBus.RegisterAllHandlersInAssembly(assembly, handlerType => kernel.Get(handlerType));
            return eventBus;
        }

    }
}
