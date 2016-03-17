﻿using System.Collections.Generic;
using System.Security.Claims;
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
                var result = await TryRegisterUserFromModel(model);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Content");
                }
                AddAllErrorInModelState(result.Errors);
            }
            return View(model);
        }

        private Task<IdentityResult> TryRegisterUserFromModel(RegisterViewModel model)
        {
            var newUser = new ApplicationUser(model.Email) { Name = model.Name };
            return UserManager.CreateAsync(newUser, model.Password);
        }

        private void AddAllErrorInModelState(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
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
                    await AuthorizeUser(user);
                    return RedirectToNextAction(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(model);
        }

        private async Task AuthorizeUser(ApplicationUser user)
        {
            var claim = await CreateClaimForUser(user);
            AuthManager.SignOut();
            var authProperties = new AuthenticationProperties { IsPersistent = true };
            AuthManager.SignIn(authProperties, claim);
        }

        private Task<ClaimsIdentity> CreateClaimForUser(ApplicationUser user)
        {
            return UserManager.ClaimsIdentityFactory
                .CreateAsync(UserManager, user, DefaultAuthenticationTypes.ApplicationCookie);
        }

        private ActionResult RedirectToNextAction(string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("List", "Content");
            }
            return Redirect(returnUrl);
        }

        public ActionResult Logout(string returnUrl)
        {
            AuthManager.SignOut();
            return RedirectToAction("List", "Content");
        }
    }
}