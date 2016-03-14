using System.Web.Mvc;
using System.Web.Routing;

namespace InternetShop.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: null,
                url: "Page{page}",
                defaults: new {controller = "Content", action = "List"}
                );

            routes.MapRoute(
                name: null, 
                url: "Admin",
                defaults: new {controller = "Admin", action = "ProductList"}
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Content", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
