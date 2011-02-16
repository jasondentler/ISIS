using System;
using System.Collections.Generic;
using ISIS.Infrastructure;

namespace ISIS
{
    public class CourseListDenormalizer
        : Denormalizer<CourseCreatedEvent, CourseList, Guid>
    {

        protected override IEnumerable<ColumnInformation> GetDataColumns()
        {
            yield return new ColumnInformation<string>("Rubric");
            yield return new ColumnInformation<string>("Number");
            yield return new ColumnInformation<string>("Title");
        }
        
        public override void Handle(CourseCreatedEvent evnt)
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
