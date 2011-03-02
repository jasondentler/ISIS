using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class CreateTermCommand : CommandBase
    {

        public string Name { get; set;}

    }
}