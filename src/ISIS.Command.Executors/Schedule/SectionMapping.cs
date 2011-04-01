using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs.Domain;

namespace ISIS.Schedule
{
    public class SectionMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {
            Map.Command<CreateSectionCommand>()
                .ToAggregateRoot<Section>()
                .CreateNew(cmd =>
                               {
                                   var uow = UnitOfWorkContext.Current;
                                   var course = (Course) uow.GetById(typeof (Course), cmd.CourseId, null);
                                   var term = (Term)uow.GetById(typeof(Term), cmd.TermId, null);
                                   return new Section(cmd.SectionId, term, course, cmd.SectionNumber);
                               })
                               .RegisterWith(commandService);
        }

    }
}
