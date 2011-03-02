using System;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeCIPCommand : CommandBase
    {

        public Guid CourseId { get;  set; }
        public string CIP { get;  set; }
    }
}