using System;
using System.Collections.Generic;
using ISIS.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace ISIS
{
    public class CourseListDenormalizer : Denormalizer<CourseList, Guid>, 
        IEventHandler<CourseCreatedEvent>
    {

        protected override IEnumerable<ColumnInformation> GetDataColumns()
        {
            yield return new ColumnInformation<string>("Rubric");
            yield return new ColumnInformation<string>("Number");
            yield return new ColumnInformation<string>("Title");
        }
        
        public void Handle(CourseCreatedEvent evnt)
        {
            var tbl = new CourseList();
            tbl.Insert(new
                           {
                               ID = evnt.EventSourceId,
                               evnt.Rubric,
                               evnt.Number,
                               evnt.Title
                           });
        }

    }
}
