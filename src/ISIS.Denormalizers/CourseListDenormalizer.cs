using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseListDenormalizer : 
        Denormalizer, 
        IEventHandler<CourseCreatedEvent>
    {
        
        public CourseListDenormalizer(IRepositoryFactory repositoryFactory)
            : base(repositoryFactory)
        {
        }

        public void Handle(CourseCreatedEvent evnt)
        {
            using (var repo = OpenRepository())
                repo.Insert(new CourseList()
                                {
                                    Number = evnt.Number,
                                    Id = evnt.EventSourceId,
                                    Rubric = evnt.Rubric,
                                    Title = evnt.Title
                                });
        }

    }
}
