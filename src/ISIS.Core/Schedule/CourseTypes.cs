using System.ComponentModel.DataAnnotations;

namespace ISIS.Schedule
{

    public enum CourseTypes
    {
        [Display(Name = "General Academic")]
        ACAD,

        [Display(Name = "Regular Technical")]
        TECH,

        [Display(Name = "Technical in WECM")]
        WECM,

        [Display(Name = "Non-funded")]
        NF,

        [Display(Name = "Non-reported Lab Course")]
        NLAB,

        [Display(Name = "Non-funded developmental")]
        NFDEV,

        [Display(Name = "Non-funded ROTC")]
        NROTC,

        [Display(Name = "Exempt from Rider 50")]
        R50
    }
}
