using System;
using System.Collections.Generic;

namespace ISIS.Schedule
{
    public class LookupCourseTypesList : IListQuery<CourseTypesList>
    {
        private readonly Guid _courseId;


        public LookupCourseTypesList(Guid courseId)
        {
            _courseId = courseId;
        }

        public string QueryName
        {
            get { return GetType().ToString(); }
        }

        public Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object>()
                       {
                           {"CourseId", _courseId}
                       };
        }
    }
}
