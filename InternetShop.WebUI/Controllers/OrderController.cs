using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Models.UserInfoModels;
using Microsoft.AspNet.Identity;

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

        public async Task<ActionResult> UserInfo()
        {
            var user = await usersRepository.GetUserByIdWithOrders(User.Identity.GetUserId());
            var userInfoModel = UserInfoModel.Create(user);
            return View(userInfoModel);
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
    }
}