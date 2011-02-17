using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseListDenormalizer : 
        Denormalizer, 
        IEventHandler<CourseCreatedEvent>
    {
        private readonly IRepository _repository;

        public CourseListDenormalizer(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CourseCreatedEvent evnt)
        {
                _repository.Insert(new CourseList()
                                {
                                    Number = evnt.Number,
                                    Id = evnt.EventSourceId,
                                    Rubric = evnt.Rubric,
                                    Title = evnt.Title
                                });
        }

    }
}
