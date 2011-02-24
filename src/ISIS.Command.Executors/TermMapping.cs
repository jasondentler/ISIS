using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public class TermMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateTermCommand>()
                .ToAggregateRoot<Term>()
                .CreateNew(cmd => new Term(cmd.Name))
                .RegisterWith(commandService);

        }
    }
}
