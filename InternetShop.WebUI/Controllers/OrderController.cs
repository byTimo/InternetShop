using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Models.UserInfoModels;

namespace InternetShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IOrderesRepository orderesRepository;

        public OrderController(IOrderesRepository orderesRepository)
        {
            this.orderesRepository = orderesRepository;
        }

        public ActionResult UserInfo(User user)
        {
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
            return RedirectToAction("UserInfo");
        }
    }
}