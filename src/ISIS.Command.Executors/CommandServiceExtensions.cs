using System;
using System.Linq;
using System.Reflection;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public static class CommandServiceExtensions
    {

        public static void Configure(this CommandService commandService, ICommandMapping commandMapping)
        {
            commandMapping.MapCommands(commandService);
        }

        public static void Configure(this CommandService commandService, params Type[] commandMappingTypes)
        {
            foreach (var commandMappingType in commandMappingTypes)
            {
                var commandMapping = (ICommandMapping) Activator.CreateInstance(commandMappingType);
                commandService.Configure(commandMapping);
            }
        }

        public static void Configure(this CommandService commandService, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => typeof (ICommandMapping).IsAssignableFrom(t)
                            && t.IsClass && !t.IsAbstract)
                .ToArray();
            commandService.Configure(types);
        }

        public static void Configure(this CommandService commandService)
        {
            commandService.Configure(Assembly.GetExecutingAssembly());
        }

    }
}
