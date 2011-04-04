using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Schedule
{
    public class TermMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateTermCommand>()
                .ToAggregateRoot<Term>()
                .CreateNew(cmd => new Term(cmd.TermId,
                                           cmd.Abbreviation,
                                           cmd.Name,
                                           cmd.StartDate,
                                           cmd.EndDate))
                .RegisterWith(commandService);
        }

    }
}
