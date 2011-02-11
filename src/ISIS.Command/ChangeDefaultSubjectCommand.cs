using System;
using Ncqrs.Commanding;

namespace ISIS
{

    public class ChangeDefaultSubjectCommand : CommandBase 
    {

        public Guid DepartmentId { get; set; }
        public string DefaultSubject { get; set; }

    }
}
