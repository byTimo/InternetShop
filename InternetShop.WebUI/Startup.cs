using System;
using System.Threading.Tasks;
using InternetShop.WebUI.Infrastructure.AcountInfrastructure;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(InternetShop.WebUI.Startup))]

namespace InternetShop.WebUI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext(InternetShopUserManager.Create);
        }
    }
}
