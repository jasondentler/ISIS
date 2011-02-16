using System.Web.Mvc;

namespace ISIS.Web.Areas.Schedule
{
    public class ScheduleAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Schedule";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Schedule_default",
                "Schedule/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
