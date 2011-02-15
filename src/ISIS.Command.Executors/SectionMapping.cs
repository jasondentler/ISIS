using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public class SectionMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateSectionCommand>()
                .ToAggregateRoot<Section>()
                .CreateNew(cmd => new Section(cmd.CourseId, cmd.TermId, cmd.SectionNumber))
                .StoreInDomainRepository()
                .RegisterWith(commandService);

        }
    }
}
