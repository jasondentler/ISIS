using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution.Mapping.Attributes;

namespace ISIS
{

    [MapsToAggregateRootConstructor(typeof(Department))]
    public class CreateDepartmentCommand : CommandBase 
    {

        public string Name { get; set; }

    }
}
