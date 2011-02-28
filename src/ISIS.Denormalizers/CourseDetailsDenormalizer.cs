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
        IEventHandler<CourseCIPAssignedEvent>,
        IEventHandler<CourseApprovalNumberAssignedEvent>
    {
        
        public CourseDetailsDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CourseCreatedEvent>();
            CreateMap<CourseTitleChangedEvent>();
            CreateMap<CourseLongTitleChangedEvent>();
            CreateMap<CourseDescriptionChangedEvent>();
            CreateMap<CourseCIPAssignedEvent>();
            CreateMap<CourseApprovalNumberAssignedEvent>();
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

        public void Handle(IPublishedEvent<CourseCIPAssignedEvent> evnt)
        {
            Upsert(evnt);
        }

        public void Handle(IPublishedEvent<CourseApprovalNumberAssignedEvent> evnt)
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
