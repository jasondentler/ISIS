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
        IEventHandler<CourseApprovalNumberChangedEvent>,
        IEventHandler<CourseActivatedEvent>,
        IEventHandler<CourseDeactivatedEvent>,
        IEventHandler<CourseMadePendingEvent>
    {
        
        public CourseDetailsDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CourseCreatedEvent>()
                .ForMember(c => c.Status, mo => mo.UseValue(CourseStatuses.Active));

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

        private void SetStatus(Guid courseId, CourseStatuses status)
        {
            var cmd = Upsert()
                .Set(c => c.Status, status)
                .Where(c => c.CourseId == courseId)
                .ToCommand();
            Execute(cmd);
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

        public void Handle(IPublishedEvent<CourseActivatedEvent> evnt)
        {
            SetStatus(evnt.Payload.CourseId, CourseStatuses.Active);
        }

        public void Handle(IPublishedEvent<CourseDeactivatedEvent> evnt)
        {
            SetStatus(evnt.Payload.CourseId, CourseStatuses.Inactive);
        }

        public void Handle(IPublishedEvent<CourseMadePendingEvent> evnt)
        {
            SetStatus(evnt.Payload.CourseId, CourseStatuses.Pending);
        }
    }
}
