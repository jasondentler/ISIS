using System;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS.Schedule
{
    public class CourseSetDenormalizer : 
        Denormalizer<CourseSet>, 
        IEventHandler<CourseCreatedEvent>
    {

        public CourseSetDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CourseCreatedEvent>();
        }

        protected override Expression<Func<CourseSet, object>> GetId()
        {
            return GetId(c => c.CourseId);
        }

        public void Handle(IPublishedEvent<CourseCreatedEvent> evnt)
        {
            Insert(evnt);
        }
        
    }
}
