using System.Threading.Tasks;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models.UserInfoModels;

namespace InternetShop.WebUI.Controllers
{
    public class OrderController : Controller
    {
        private IUsersRepository usersRepository;

        public OrderController(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<ActionResult> UserInfo(string userId)
        {
            var user = await usersRepository.GetUserByIdWithOrders(userId);
            var userInfoModel = UserInfoModel.Create(user);
            return View(userInfoModel);
        }

    }
}