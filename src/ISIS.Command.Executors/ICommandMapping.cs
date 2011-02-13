using Ncqrs.Commanding.ServiceModel;

namespace ISIS
{
    public interface ICommandMapping
    {

        void MapCommands(CommandService commandService);

    }
}
