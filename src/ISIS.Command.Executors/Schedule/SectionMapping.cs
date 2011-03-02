using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Schedule
{
    public class SectionMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateSectionCommand>()
                .ToAggregateRoot<Section>()
                .CreateNew(cmd => new Section(cmd.CourseId, cmd.TermId, cmd.Number))
                .RegisterWith(commandService);

        }
    }
}
