using System;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS.Schedule
{
    public class CourseListDenormalizer : 
        Denormalizer<CourseList>, 
        IEventHandler<CreditCourseCreatedEvent>,
        IEventHandler<CourseTitleChangedEvent>
    {
        
        public CourseListDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CreditCourseCreatedEvent>();
            CreateMap<CourseTitleChangedEvent>();
        }

        protected override Expression<Func<CourseList, object>> GetId()
        {
            return GetId(c => c.CourseId);
        }

        public void Handle(IPublishedEvent<CreditCourseCreatedEvent> evnt)
        {
            Upsert(evnt);
        }

        public void Handle(IPublishedEvent<CourseTitleChangedEvent> evnt)
        {
            Upsert(evnt);
        }

    }
}
