using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Commanding.CommandExecution.Mapping.Fluent;
using Ncqrs.Commanding.ServiceModel;

namespace ISIS.Schedule
{
    public class TopicCodeMapping : ICommandMapping
    {
        public void MapCommands(CommandService commandService)
        {

            Map.Command<CreateTopicCodeCommand>()
                .ToAggregateRoot<TopicCode>()
                .CreateNew(cmd => new TopicCode(cmd.Abbreviation, cmd.Description))
                .RegisterWith(commandService);

            Map.Command<ChangeTopicCodeAbbreviationCommand>()
                .ToAggregateRoot<TopicCode>()
                .WithId(cmd => cmd.TopicCodeId)
                .ToCallOn((cmd, topicCode) => topicCode.ChangeAbbreviation(cmd.Abbreviation))
                .RegisterWith(commandService);

            Map.Command<ChangeTopicCodeDescriptionCommand>()
                .ToAggregateRoot<TopicCode>()
                .WithId(cmd => cmd.TopicCodeId)
                .ToCallOn((cmd, topicCode) => topicCode.ChangeDescription(cmd.Description))
                .RegisterWith(commandService);

        }

    }
}
