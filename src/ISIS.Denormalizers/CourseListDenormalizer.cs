using System;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseListDenormalizer : 
        Denormalizer<CourseList>, 
        IEventHandler<CourseCreatedEvent>,
        IEventHandler<CourseTitleChangedEvent>
    {
        
        public CourseListDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CourseCreatedEvent>();
            CreateMap<CourseTitleChangedEvent>();
        }

        protected override Expression<Func<CourseList, object>> GetId()
        {
            return GetId(c => c.CourseId);
        }

        public void Handle(IPublishedEvent<CourseCreatedEvent> evnt)
        {
            Upsert(evnt);
        }

        public void Handle(IPublishedEvent<CourseTitleChangedEvent> evnt)
        {
            Upsert(evnt);
        }

    }
}
