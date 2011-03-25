using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class CreateTopicCodeCommand : CommandBase 
    {

        public string Abbreviation { get; set; }
        public string Description { get; set; }

    }

}
