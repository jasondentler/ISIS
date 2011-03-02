using System.Collections.Generic;

namespace ISIS.Schedule
{
    public class LookupCourseSet : IListQuery<CourseSet>
    {
        private readonly string _rubric;
        private readonly string _number;

        public LookupCourseSet(string rubric, string number)
        {
            _rubric = rubric;
            _number = number;
        }

        public string QueryName
        {
            get { return typeof (LookupCourseSet).ToString(); }
        }

        public Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object>()
                       {
                           {"Rubric", _rubric},
                           {"Number", _number}
                       };
        }
    }
}
