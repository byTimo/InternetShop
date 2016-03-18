using System.Web.Mvc;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Models.UserInfoModels;

namespace InternetShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult UserInfo(User user)
        {
            var userInfoModel = UserInfoModel.Create(user);
            return View(userInfoModel);
        }
    }
}