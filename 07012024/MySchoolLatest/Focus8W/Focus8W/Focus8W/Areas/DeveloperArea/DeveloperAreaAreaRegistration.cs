using System.Web.Mvc;

namespace Focus8W.Areas.DeveloperArea
{
    public class DeveloperAreaAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "DeveloperArea";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "DeveloperArea_default",
                "DeveloperArea/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}