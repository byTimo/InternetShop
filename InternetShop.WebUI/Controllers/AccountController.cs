using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InternetShop.WebUI.Infrastructure.AccountInfrastructure;
using InternetShop.WebUI.Models;
using InternetShop.WebUI.Models.AccountModels;
using Microsoft.AspNet.Identity.Owin;

namespace InternetShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private InternetShopUserManager userManager => HttpContext.GetOwinContext().GetUserManager<InternetShopUserManager>();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newUser = new ApplicationUser(model.Name) {Email = model.Email};
                var result = await userManager.CreateAsync(newUser, model.Password);
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
    }
}