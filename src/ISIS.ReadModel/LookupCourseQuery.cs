using System.Collections.Generic;

namespace ISIS
{
    public class LookupCourseQuery : IListQuery<CourseDetails>
    {

        public string Rubric { get; private set; }
        public string Number { get; private set; }
        
        public LookupCourseQuery(string rubric, string number)
        {
            Rubric = rubric;
            Number = number;
        }

        string IQuery.QueryName
        {
            get { return typeof (LookupCourseQuery).Name; }
        }

        Dictionary<string, object> IQuery.GetParameters()
        {
            return new Dictionary<string, object>()
                       {
                           {"Rubric", Rubric},
                           {"Number", Number}
                       };
        }

    }
}
