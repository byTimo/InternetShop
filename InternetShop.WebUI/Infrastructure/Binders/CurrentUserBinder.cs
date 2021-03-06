﻿using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using Microsoft.AspNet.Identity;

namespace InternetShop.WebUI.Infrastructure.Binders
{
    public class CurrentUserBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var repository = DependencyResolver.Current.GetService<IUsersRepository>();

            User user = null;
            if (controllerContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var currentUserId = controllerContext.HttpContext.User.Identity.GetUserId();
                var dbResult = repository.GetUserById(currentUserId).Result;
                if (dbResult.IsSucceeded)
                    user = dbResult.Result;
            }
            return user;
        }
    }
}