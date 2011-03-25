using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeTopicCodeDescriptionCommand : CommandBase 
    {

        public Guid TopicCodeId { get; set; }
        public string Description { get; set; }

    }
}
