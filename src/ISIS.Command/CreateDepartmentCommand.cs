using Ncqrs.Commanding;

namespace ISIS
{

    public class CreateDepartmentCommand : CommandBase 
    {

        public string Name { get; set; }

    }
}
