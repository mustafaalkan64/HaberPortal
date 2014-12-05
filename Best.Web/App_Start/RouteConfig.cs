using System.Web.Mvc;
using System.Web.Routing;

namespace Best.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(name: "Robots.txt",
                url: "robots.txt",
                defaults: new { controller = "Common", action = "Robots" },
                namespaces: new[] { "Best.Web.Controllers" });

            routes.MapRoute(
                name: "GaleryDetail",
                url: "GaleriDetay/{id}/{seoName}/{gridPage}",
                defaults: new
                {
                    controller = "Galery",
                    action = "GaleryDetail",
                    id = UrlParameter.Optional,
                    seoName = UrlParameter.Optional,
                    gridPage = UrlParameter.Optional
                },
                namespaces: new[] { "Best.Web.Controllers" }
            );

            routes.MapRoute(
                name: "CategoryDetail",
                url: "KategoriDetay/{category}/{id}",
                defaults: new
                {
                    controller = "Category",
                    action = "CategoryDetail",
                    id = UrlParameter.Optional,
                    category = UrlParameter.Optional
                },
                namespaces: new[] { "Best.Web.Controllers" }
            );

            routes.MapRoute(
                name: "NewsDetail",
                url: "HaberDetay/{category}/{title}/{id}",
                defaults: new
                {
                    controller = "News",
                    action = "NewsDetail",
                    id = UrlParameter.Optional,
                    category = UrlParameter.Optional,
                    title = UrlParameter.Optional
                },
                namespaces: new[] { "Best.Web.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "Best.Web.Controllers" }
            );
        }
    }
}
