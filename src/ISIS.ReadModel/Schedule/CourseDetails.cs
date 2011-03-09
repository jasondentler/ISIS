using System;
using System.ComponentModel.DataAnnotations;

namespace ISIS.Schedule
{
    public class CourseDetails : IEntity
    {

        [Id]
        public Guid CourseId { get; set; }

        public string Rubric { get; set; }

        public string Number { get; set; }

        public string Title { get; set; }

        [Display(Name="Long Title")]
        public string LongTitle { get; set; }

        public string Description { get; set; }

        public string CIP { get; set; }

        [Display(Name = "Approval Number")]
        public string ApprovalNumber { get; set; }

        public CourseStatuses Status { get; set; }

        [Display(Name = "Course Types")]
        public string CourseTypes { get; set; }

    }
}
