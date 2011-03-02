using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Schedule
{
    public class DepartmentMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateDepartmentCommand>()
                .ToAggregateRoot<Department>()
                .CreateNew(cmd => new Department(cmd.Name))
                .RegisterWith(commandService);

        }
    }
}
