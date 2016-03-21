﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Models.AccountModels;
using InternetShop.WebUI.Models.OrderModels;
using InternetShop.WebUI.Models.UserInfoModels;

namespace InternetShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderesRepository orderesRepository;
        private readonly IUsersRepository usersRepository;

        public OrderController(IOrderesRepository orderesRepository, IUsersRepository usersRepository)
        {
            this.orderesRepository = orderesRepository;
            this.usersRepository = usersRepository;
        }

        public ActionResult UserInfo(User user)
        {
            var userInfoModel = UserInfoModel.Create(user);
            return View(userInfoModel);
        }

        public ActionResult EditUserInfo(User user)
        {
            var userViewModel = UserViewModel.Create(user);
            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserInfo(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = model.ToUser();
                await usersRepository.UpdateUser(user);
                return RedirectToAction("UserInfo");
            }
            TempData["error-message"] = "При сохранении возникли ошибки!";
            return View(model);
        }

        public async Task<ActionResult> CreateOrder(Cart cart, User user)
        {
            if (cart.ProductsInCart.Count == 0)
                return RedirectToAction("Cart", "Content");
            if(string.IsNullOrEmpty(user.Surname) || string.IsNullOrEmpty(user.Address))
                throw new NotImplementedException();

            await Task.Run(() => orderesRepository.CreateOrder(user, cart.ProductsInCart));
            cart.Clear();
            return RedirectToAction("UserInfo");
        }

        public async Task<ActionResult> OrderInfo(int orderId)
        {
            var order = await orderesRepository.GetOrderIncludeAllbyId(orderId);
            var orderInfoModel = OrderInfoModel.Create(order);
            return PartialView(orderInfoModel);
        }
    }
}