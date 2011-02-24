using System;
using System.Linq.Expressions;
using AutoMapper;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseDetailsDenormalizer : 
        Denormalizer<CourseDetails>, 
        IEventHandler<CourseCreatedEvent>,
        IEventHandler<CourseTitleChangedEvent>,
        IEventHandler<CourseLongTitleChangedEvent>,
        IEventHandler<CourseCIPAssignedEvent>,
        IEventHandler<CourseApprovalNumberAssignedEvent>
    {
        
        public CourseDetailsDenormalizer(IDialect db)
            : base(db)
        {
            CreateMap<CourseCreatedEvent>()
                .ForMember(c => c.Title, mo => mo.Ignore())
                .ForMember(c => c.LongTitle, mo => mo.Ignore())
                .ForMember(c => c.CIP, mo => mo.Ignore())
                .ForMember(c => c.ApprovalNumber, mo => mo.Ignore());

            CreateMap<CourseTitleChangedEvent>()
                .ForMember(c => c.Rubric, mo => mo.Ignore())
                .ForMember(c => c.Number, mo => mo.Ignore())
                .ForMember(c => c.LongTitle, mo => mo.Ignore())
                .ForMember(c => c.CIP, mo => mo.Ignore())
                .ForMember(c => c.ApprovalNumber, mo => mo.Ignore());

            CreateMap<CourseLongTitleChangedEvent>()
                .ForMember(c => c.Rubric, mo => mo.Ignore())
                .ForMember(c => c.Number, mo => mo.Ignore())
                .ForMember(c => c.Title, mo => mo.Ignore())
                .ForMember(c => c.CIP, mo => mo.Ignore())
                .ForMember(c => c.ApprovalNumber, mo => mo.Ignore());

            CreateMap<CourseCIPAssignedEvent>()
                .ForMember(c => c.Rubric, mo => mo.Ignore())
                .ForMember(c => c.Number, mo => mo.Ignore())
                .ForMember(c => c.Title, mo => mo.Ignore())
                .ForMember(c => c.LongTitle, mo => mo.Ignore())
                .ForMember(c => c.ApprovalNumber, mo => mo.Ignore());

            CreateMap<CourseApprovalNumberAssignedEvent>()
                .ForMember(c => c.Rubric, mo => mo.Ignore())
                .ForMember(c => c.Number, mo => mo.Ignore())
                .ForMember(c => c.Title, mo => mo.Ignore())
                .ForMember(c => c.LongTitle, mo => mo.Ignore())
                .ForMember(c => c.CIP, mo => mo.Ignore());
           
            Mapper.AssertConfigurationIsValid();
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
    }
}
