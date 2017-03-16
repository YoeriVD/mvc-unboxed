using System.Web;
using System.Web.Mvc;
using unboxed.web.Infrastructure;

namespace unboxed.web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new TrackTime());
        }
    }
}
