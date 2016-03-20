using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Infrastructure.AccountInfrastructure;
using InternetShop.WebUI.Models.AccountModels;
using InternetShop.WebUI.Models.OrderModels;
using InternetShop.WebUI.Models.ProductModels;
using Microsoft.AspNet.Identity.Owin;

namespace InternetShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IProductsRepository productRepository;
        private readonly IUsersRepository usersRepository;
        private readonly IOrderesRepository orderesRepository;

        private InternetShopUserManager UserManager
            => HttpContext.GetOwinContext().GetUserManager<InternetShopUserManager>();

        public AdminController(IProductsRepository productRepository, IUsersRepository usersRepository, IOrderesRepository orderesRepository)
        {
            this.productRepository = productRepository;
            this.usersRepository = usersRepository;
            this.orderesRepository = orderesRepository;
        }

        public ViewResult ProductList()
        {
            var allProducts = new ProductListViewModel
            {
                Products = productRepository.Products.Select(ProductViewModel.Create)
            };
            return View(allProducts);
        }

        public ViewResult CreateProduct()
        {
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                productRepository.SaveProduct(productViewModel.ToProduct());
                TempData["message"] = $"Добавлен новый товар: {productViewModel.Name}";
                return RedirectToAction("ProductList");
            }
            TempData["error-message"] = "При создании товара возникли ошибки";
            return View(productViewModel);
        }

        public ViewResult EditProduct(int productId)
        {
            ViewBag.Title = "Админ панель : изменение товара";
            var product = productRepository.Products.First(p => p.ProductId == productId);
            var productViewModel = ProductViewModel.Create(product);
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = productViewModel.ToProduct();
                productRepository.SaveProduct(product);
                TempData["message"] = $"Изменения товара {product.Name} были сохранены!";
                return RedirectToAction("ProductList");
            }
            TempData["error-message"] = "При обновлении товара возникли ошибки!";
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var product = productRepository.Products.First(p => p.ProductId == productId);
            productRepository.DeleteProduct(product);
            TempData["message"] = $"Товар {product.Name} успешно удалён!";
            return RedirectToAction("ProductList");
        }

        public ActionResult UserList()
        {
            var users = usersRepository.Users.Select(UserViewModel.Create);
            return View(users);
        }

        public ActionResult CreateUser()
        {
            return View(new UserViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = model.ToIdentityUser();
                var result = await UserManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    TempData["message"] = "Пользователь успешно создан!";
                    return RedirectToAction("UserList");
                }
                AddAllErrorsToModelState(result.Errors);
            }
            TempData["error-message"] = "Возникли ошибки при создании пользователя";
            return View(model);
        }

        private void AddAllErrorsToModelState(IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public ActionResult EditUser(string userId)
        {
            var user = usersRepository.Users.First(u => u.UserId.Equals(userId));
            var userModel = UserViewModel.Create(user);
            return View(userModel);
        }

        [HttpPost]
        public async Task<ActionResult> EditUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var changingUser = model.ToIdentityUser();
                var result = await UserManager.UpdateAsync(changingUser);
                if (result.Succeeded)
                {
                    TempData["message"] = "Пользователь успешно изменён!";
                    return RedirectToAction("UserList");
                }
                AddAllErrorsToModelState(result.Errors);
            }
            TempData["error-message"] = "Возникли ошибки при изменении пользователя!";
            return View(model);
        }

        public ActionResult OrderList()
        {
            var orders = orderesRepository.Orders.Select(OrderInfoModel.Create);
            return View(orders);
        }

        protected override void Dispose(bool disposing)
        {
            productRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}