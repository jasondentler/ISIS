using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class ChangeTopicCodeAbbreviationCommand : CommandBase 
    {

        public Guid TopicCodeId { get; set; }
        public string Abbreviation { get; set; }

    }

}
