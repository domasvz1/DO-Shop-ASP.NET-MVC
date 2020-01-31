using System.Web.Mvc;
using System.Web.Routing;

namespace Presentation
{
    public class RouteConfig
    {
        static RouteCollection currentRoutes;

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Main", action = "Index", id = UrlParameter.Optional }
            );
            currentRoutes = routes;
        }
        public static RouteCollection GetRoute()
        {
            return currentRoutes;
        }
    }
}

