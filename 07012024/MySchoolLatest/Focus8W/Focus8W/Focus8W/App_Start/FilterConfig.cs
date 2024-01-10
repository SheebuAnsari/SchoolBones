//using Focus8W.GlobalFactory;
using System.Web;
using System.Web.Mvc;

namespace Focus8W
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
