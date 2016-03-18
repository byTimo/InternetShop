using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models.AccountModels;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Infrastructure.AccountInfrastructure
{
    public class InternetShopUserManager : UserManager<IdentityUser>
    {
        public InternetShopUserManager(IUserStore<IdentityUser> store) : base(store)
        {
        }

        public static InternetShopUserManager Create()
        {
            return
                new InternetShopUserManager(
                    new InternetShopUserStore(DependencyResolver.Current.GetService<IUsersRepository>()));
        }
    }
}