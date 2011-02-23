using System;
using System.Linq;
using System.Reflection;
using ISIS.Validation;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public static class CommandServiceExtensions
    {
        public static CommandService Configure(this CommandService commandService)
        {
            return commandService.MapCommands();
        }

        public static CommandService MapCommands(this CommandService commandService, ICommandMapping commandMapping)
        {
            commandMapping.MapCommands(commandService);
            return commandService;
        }

        public static CommandService MapCommands(this CommandService commandService, params Type[] commandMappingTypes)
        {
            foreach (var commandMappingType in commandMappingTypes)
            {
                var commandMapping = (ICommandMapping) Activator.CreateInstance(commandMappingType);
                commandService.MapCommands(commandMapping);
            }
            return commandService;
        }

        public static CommandService MapCommands(this CommandService commandService, Assembly assembly)
        {
            var types = assembly.GetTypes()
                .Where(t => typeof (ICommandMapping).IsAssignableFrom(t)
                            && t.IsClass && !t.IsAbstract)
                .ToArray();
            return commandService.MapCommands(types);
        }

        public static CommandService MapCommands(this CommandService commandService)
        {
            return commandService.MapCommands(Assembly.GetExecutingAssembly());
        }

    }
}
