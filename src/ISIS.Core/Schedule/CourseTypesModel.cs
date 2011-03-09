using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ISIS.Schedule
{
    public class CourseTypesModel
    {
        [Display(Name = "Course")]
        public Guid CourseId { get; set; }

        [Display(Name = "Course Types")]
        public IEnumerable<CourseTypes> CourseTypes { get; set; }

    }
}
