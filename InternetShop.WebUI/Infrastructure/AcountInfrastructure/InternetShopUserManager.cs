using System;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models.AcountModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace InternetShop.WebUI.Infrastructure.AcountInfrastructure
{
    public class InternetShopUserManager : UserManager<ApplicationUser>
    {
        public InternetShopUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
            this.PasswordHasher = new InternetShopPasswordHasher();
        }

        public static InternetShopUserManager Create()
        {
            var test = DependencyResolver.Current.GetService<IUsersRepository>();
            return
                new InternetShopUserManager(
                    new InternetShopUserStore(test));
        }
    }
}