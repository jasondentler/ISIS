using Ncqrs.Commanding;

namespace ISIS
{

    public interface ISetValidator<TCommand>
        where TCommand : ICommand
    {

        void Validate(TCommand command);

    }

}
