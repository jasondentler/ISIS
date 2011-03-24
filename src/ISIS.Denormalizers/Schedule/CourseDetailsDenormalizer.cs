using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS.Schedule
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
        IEventHandler<CourseMadePendingEvent>, 
        IEventHandler<CourseMadeObsoleteEvent>,
        IEventHandler<CourseTypeAddedToCourseEvent>,
        IEventHandler<CourseTypeRemovedFromCourseEvent>
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

        public void Handle(IPublishedEvent<CourseMadeObsoleteEvent> evnt)
        {
            SetStatus(evnt.Payload.CourseId, CourseStatuses.Obsolete);
        }

        private void SetCourseTypes(Guid courseId, IEnumerable<CourseTypes> currentCourseTypes)
        {
            var enums = currentCourseTypes.Select(ct => (Enum) ct);
            var courseTypeStrings = EnumData.GetNamesForValues(typeof(CourseTypes), enums);
            var courseTypesString = string.Join(", ", courseTypeStrings);

            var cmd = Update()
                .Set(d => d.CourseTypes, courseTypesString)
                .Where(cd => cd.CourseId == courseId)
                .ToCommand();
            Execute(cmd);
        }

        public void Handle(IPublishedEvent<CourseTypeAddedToCourseEvent> evnt)
        {
            SetCourseTypes(evnt.Payload.CourseId, evnt.Payload.CurrentTypes);
        }

        public void Handle(IPublishedEvent<CourseTypeRemovedFromCourseEvent> evnt)
        {
            SetCourseTypes(evnt.Payload.CourseId, evnt.Payload.CurrentTypes);
        }
    }
}
