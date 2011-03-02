using Ncqrs.Commanding;

namespace ISIS.Schedule
{

    public class CreateDepartmentCommand : CommandBase 
    {

        public string Name { get; set; }

    }
}
