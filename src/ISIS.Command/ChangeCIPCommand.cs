using System;
using Ncqrs.Commanding;

namespace ISIS
{
    public class ChangeCIPCommand : CommandBase
    {

        public Guid CourseId { get;  set; }
        public string CIP { get;  set; }
    }
}