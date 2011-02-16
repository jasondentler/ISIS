using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;

namespace ISIS.Infrastructure
{

    public interface IDenormalizer
    {

        void Setup();
        void TryTeardown();

    }

    public interface IDenormalizer<T> : IDenormalizer, IEventHandler<T>
        where T : ISourcedEvent 
    {


    }
}
