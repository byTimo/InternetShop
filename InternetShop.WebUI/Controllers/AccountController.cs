using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InternetShop.WebUI.Infrastructure.AccountInfrastructure;
using InternetShop.WebUI.Models;
using InternetShop.WebUI.Models.AccountModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace InternetShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private InternetShopUserManager UserManager => HttpContext.GetOwinContext().GetUserManager<InternetShopUserManager>();
        private IAuthenticationManager AuthManager => HttpContext.GetOwinContext().Authentication;

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser(model.Email) {Name = model.Name};
                var result = await UserManager.CreateAsync(newUser, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Content");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }   
                }
            }
            return View(model);
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindAsync(model.Email, model.Password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    var claim =
                        await
                            UserManager.ClaimsIdentityFactory.CreateAsync(UserManager, user,
                                DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    if (string.IsNullOrEmpty(returnUrl))
                    {
                        return RedirectToAction("List", "Content");
                    }
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        public ActionResult Logout(string returnUrl)
        {
            AuthManager.SignOut();
            return RedirectToAction("List", "Content");
        }
    }
}