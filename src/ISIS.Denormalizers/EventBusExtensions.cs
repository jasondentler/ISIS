using System;
using System.Linq;
using System.Reflection;
using Ncqrs.Eventing;
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
            return eventBus.RegisterDenormalizers(kernel, assembly.GetTypes());
        }

        public static IEventBus RegisterDenormalizers(this InProcessEventBus eventBus, IKernel kernel, params Type[] types)
        {
            var denormalizerTypes = types.Where(ImplementsAtLeastOneIEventHandlerInterface).Distinct();
            foreach (var denormalizerType in denormalizerTypes)
            {
                foreach (var handlerType in denormalizerType.GetInterfaces().Where(IsIEventHandlerInterface))
                {
                    var denormalizerType2 = denormalizerType;
                    var eventType = handlerType.GetGenericArguments().Single();
                    eventBus.RegisterHandler(eventType, evnt => Handle(kernel, denormalizerType2, evnt));
                }
            }
            return eventBus;
        }

        private static void Handle(IKernel kernel, Type denormalizerType, IEvent @event)
        {
            var handler = (dynamic) kernel.Get(denormalizerType);
            handler.Handle((dynamic) @event);
        }

        private static void RegisterHandlerFactory<TEvent>(this InProcessEventBus eventBus, Func<IEventHandler<TEvent>> handlerFactory)
            where TEvent : IEvent
        {
            //var eventDataType = typeof(TEvent);
            //Action<IPublishableEvent> handler = evnt =>
            //                             {
            //                                 var publishedEvent = new PublishedEvent<TEvent>(evnt);
            //                                 handlerFactory().Handle((TEvent) evnt
            //                             })
            //eventBus.RegisterHandler(eventDataType, handler);
        }


        private static bool ImplementsAtLeastOneIEventHandlerInterface(Type type)
        {
            return type.IsClass &&
                   !type.IsAbstract &&
                   type.GetInterfaces().Any(IsIEventHandlerInterface);
        }

        private static bool IsIEventHandlerInterface(Type type)
        {
            return type.IsInterface &&
                   type.IsGenericType &&
                   type.GetGenericTypeDefinition() == typeof(IEventHandler<>);
        }

    }
}
