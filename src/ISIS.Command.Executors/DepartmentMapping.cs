using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public class DepartmentMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateDepartmentCommand>()
                .ToAggregateRoot<Department>()
                .CreateNew(cmd => new Department(cmd.Name))
                .StoreInDomainRepository()
                .RegisterWith(commandService);

        }
    }
}
