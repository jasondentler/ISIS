using System;
using System.ComponentModel.DataAnnotations;
using Ncqrs.Commanding;

namespace ISIS.Schedule
{
    public class ChangeCourseLongTitleCommand : CommandBase
    {
        public Guid CourseId { get; set; }

        [Display(Name = "New Long Title")]
        public string NewLongTitle { get; set; }
    }
}
