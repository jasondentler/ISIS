using System;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseDetailsDenormalizer : 
        Denormalizer<CourseDetails>, 
        IEventHandler<CourseCreatedEvent>,
        IEventHandler<CourseTitleChangedEvent>,
        IEventHandler<CourseLongTitleChangedEvent>,
        IEventHandler<CourseDescriptionChangedEvent>,
        IEventHandler<CourseCIPChangedEvent>,
        IEventHandler<CourseApprovalNumberChangedEvent>
    {
        
        public CourseDetailsDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CourseCreatedEvent>();
            CreateMap<CourseTitleChangedEvent>();
            CreateMap<CourseLongTitleChangedEvent>();
            CreateMap<CourseDescriptionChangedEvent>();
            CreateMap<CourseCIPChangedEvent>();
            CreateMap<CourseApprovalNumberChangedEvent>();
        }

        protected override Expression<Func<CourseDetails, object>> GetId()
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

        public void Handle(IPublishedEvent<CourseCIPChangedEvent> evnt)
        {
            Upsert(evnt);
        }

        public void Handle(IPublishedEvent<CourseApprovalNumberChangedEvent> evnt)
        {
            Upsert(evnt);
        }

        public void Handle(IPublishedEvent<CourseLongTitleChangedEvent> evnt)
        {
            Upsert(evnt);
        }

        public void Handle(IPublishedEvent<CourseDescriptionChangedEvent> evnt)
        {
            Upsert(evnt);
        }

    }
}
