using Best.Web.Infrastructure.Extensions;
using System.Web;
using System.Web.Mvc;

namespace Best.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
