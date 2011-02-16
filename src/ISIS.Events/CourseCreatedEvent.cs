﻿using Ncqrs.Eventing.Sourcing;

namespace ISIS
{
    public class CourseCreatedEvent : SourcedEvent
    {

        public string Rubric { get; set; }
        public string Number { get; set; }
        public string Title { get; set; }
        
    }
}
