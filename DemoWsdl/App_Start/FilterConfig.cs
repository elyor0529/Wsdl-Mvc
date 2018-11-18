using System.Web;
using System.Web.Mvc;
using DemoWsdl.Filters;

namespace DemoWsdl
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
