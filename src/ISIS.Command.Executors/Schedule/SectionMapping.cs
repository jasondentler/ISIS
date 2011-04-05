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
                                   var course = uow.GetById<Course>(cmd.CourseId);
                                   var term = uow.GetById<Term>(cmd.TermId);
                                   return new Section(cmd.SectionId, term, course, cmd.SectionNumber);
                               })
                .RegisterWith(commandService);

            Map.Command<ChangeSectionLocationCommand>()
                .ToAggregateRoot<Section>()
                .WithId(cmd => cmd.SectionId)
                .ToCallOn((cmd, section) =>
                              {
                                  var uow = UnitOfWorkContext.Current;
                                  var location = uow.GetById<Location>(cmd.LocationId);
                                  var tdcjTopicCode = uow.GetById<TopicCode>(cmd.TDCJTopicCodeId);
                                  section.ChangeLocation(location, tdcjTopicCode);
                              })
                .RegisterWith(commandService);

            Map.Command<ChangeSectionCreditTypeCommand>()
                .ToAggregateRoot<Section>()
                .WithId(cmd => cmd.SectionId)
                .ToCallOn((cmd, section) => section.ChangeCreditType(cmd.CreditType))
                .RegisterWith(commandService);

            Map.Command<ChangeSectionDatesCommand>()
                .ToAggregateRoot<Section>()
                .WithId(cmd => cmd.SectionId)
                .ToCallOn((cmd, section) => section.ChangeDates(cmd.StartDate, cmd.EndDate))
                .RegisterWith(commandService);

            Map.Command<ChangeSectionNumberCommand>()
                .ToAggregateRoot<Section>()
                .WithId(cmd => cmd.SectionId)
                .ToCallOn((cmd, section) => section.ChangeSectionNumber(cmd.SectionNumber))
                .RegisterWith(commandService);
        }


    }
}
