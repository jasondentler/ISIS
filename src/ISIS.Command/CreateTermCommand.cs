using Ncqrs.Commanding;

namespace ISIS
{
    public class CreateTermCommand : CommandBase
    {

        public string Name { get; set;}

    }
}