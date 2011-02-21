using System;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseListDenormalizer : 
        IDenormalizer, 
        IEventHandler<CourseCreatedEvent>,
        IEventHandler<CourseTitleChangedEvent>
    {
        private readonly IRepository _repository;

        public CourseListDenormalizer(IRepository repository)
        {
            _repository = repository;
        }

        public void Handle(IPublishedEvent<CourseCreatedEvent> evnt)
        {
            _repository.Insert(new CourseList()
            {
                Number = evnt.Payload.Number,
                Id = evnt.Payload.CourseId,
                Rubric = evnt.Payload.Rubric,
                Title = evnt.Payload.Title
            });
        }

        public void Handle(IPublishedEvent<CourseTitleChangedEvent> evnt)
        {
            var course = _repository.Single<CourseList>(evnt.EventIdentifier);
            course.Title = evnt.Payload.NewTitle;
            _repository.Update(course);
        }
    }
}
