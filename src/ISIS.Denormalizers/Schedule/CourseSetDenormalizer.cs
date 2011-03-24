using System;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS.Schedule
{
    public class CourseSetDenormalizer : 
        Denormalizer<CourseSet>, 
        IEventHandler<CreditCourseCreatedEvent>
    {

        public CourseSetDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CreditCourseCreatedEvent>();
        }

        protected override Expression<Func<CourseSet, object>> GetId()
        {
            return GetId(c => c.CourseId);
        }

        public void Handle(IPublishedEvent<CreditCourseCreatedEvent> evnt)
        {
            Insert(evnt);
        }
        
    }
}
