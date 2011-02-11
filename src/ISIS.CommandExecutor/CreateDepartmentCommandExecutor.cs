using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Domain;

namespace ISIS
{
    public class CreateDepartmentCommandExecutor : CommandExecutorBase<CreateDepartmentCommand>
    {

        protected override void ExecuteInContext(IUnitOfWorkContext context, CreateDepartmentCommand command)
        {
            var newDepartment = new Department(command.Name);
            context.Accept();
        }
    }
}
