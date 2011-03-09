using System;
using System.Linq.Expressions;
using FluentDML.Dialect;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS.Schedule
{
    public class CourseTypesListDenormalizer : 
        Denormalizer<CourseTypesList>, 
        IEventHandler<CourseTypeAddedToCourseEvent>,
        IEventHandler<CourseTypeRemovedFromCourseEvent>
    {

        public CourseTypesListDenormalizer(IDialect db)
            : base(db)
        {
        }

        protected override Expression<Func<CourseTypesList, object>> GetId()
        {
            return null;
        }


        public void Handle(IPublishedEvent<CourseTypeAddedToCourseEvent> evnt)
        {
            var courseId = evnt.Payload.CourseId;
            var courseType = evnt.Payload.TypeAdded;
            var cmd = Insert()
                .Set(c => c.Id, Guid.NewGuid())
                .Set(c => c.CourseId, courseId)
                .Set(c => c.CourseType, courseType)
                .ToCommand();
            Execute(cmd);
        }

        public void Handle(IPublishedEvent<CourseTypeRemovedFromCourseEvent> evnt)
        {
            var courseId = evnt.Payload.CourseId;
            var courseType = evnt.Payload.TypeRemoved;
            var cmd = Delete()
                .Where(c => c.CourseId == courseId && c.CourseType == courseType)
                .ToCommand();
            Execute(cmd);
        }
    }
}
