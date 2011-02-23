using System;
using System.Linq.Expressions;
using AutoMapper;
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
            CreateMap<CourseTitleChangedEvent>()
                .ForMember(c => c.Number, mo => mo.Ignore())
                .ForMember(c => c.Rubric, mo => mo.Ignore());
           
            Mapper.AssertConfigurationIsValid();
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
