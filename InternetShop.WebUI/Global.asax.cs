using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using InternetShop.DataLayer;
using InternetShop.WebUI.Infrastructure.Binders;

namespace InternetShop.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(Cart), new CartBinder());
        }
    }
}
