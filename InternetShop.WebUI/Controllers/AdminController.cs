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

        public async Task<ViewResult> ProductList()
        {
            var allProducts = new ProductListViewModel();
            var dbResult = await productRepository.GetAllProductAsync();
            if (dbResult.IsSucceeded)
            {
                allProducts.Products = dbResult.Result.Select(ProductViewModel.Create);
            }
            return View(allProducts);
        }

        public Task<ViewResult> CreateProduct()
        {
            return Task.FromResult(View(new ProductViewModel()));
        }

        [HttpPost]
        public async Task<ActionResult> CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = productViewModel.ToProduct();
                var dbResult = await productRepository.CreateProduct(product);
                if (dbResult.IsSucceeded)
                {
                    TempData["message"] = $"Добавлен новый товар: {productViewModel.Name}";
                    return RedirectToAction("ProductList");
                }
                else
                {
                    foreach (var error in dbResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            TempData["error-message"] = "При создании товара возникли ошибки";
            return View(productViewModel);
        }

        public async Task<ViewResult> EditProduct(int productId)
        {
            var dbEntity = await productRepository.GetProductById(productId);
            if (dbEntity.IsSucceeded)
            {
                return View(ProductViewModel.Create(dbEntity.Result));
            }
            return View(new ProductViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = productViewModel.ToProduct();
                var dbResult = await productRepository.UpdateProduct(product);
                if (dbResult.IsSucceeded)
                {
                    TempData["message"] = $"Изменения товара {product.Name} были сохранены!";
                    return RedirectToAction("ProductList");
                }
                else
                {
                    foreach (var error in dbResult.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }
            TempData["error-message"] = "При обновлении товара возникли ошибки!";
            return View(productViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteProduct(int productId)
        {
            var dbResult = await productRepository.DeleteProduct(productId);
            if (dbResult.IsSucceeded)
            {
                TempData["message"] = "Товар успешно удалён!";
                return RedirectToAction("ProductList");
            }
            return null;
        }

        public async Task<ActionResult> UserList()
        {
            var dbResult = await usersRepository.GetAllUsers();
            if (dbResult.IsSucceeded)
            {
                return View(dbResult.Result.Select(UserViewModel.Create));
            }
            return null;
        }

        public Task<ViewResult> CreateUser()
        {
            return Task.FromResult(View(new UserViewModel()));
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var appUser = model.ToUser();
                appUser.PasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                var result = await usersRepository.CreateUser(appUser);
                if (result.IsSucceeded)
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

        public async Task<ActionResult> EditUser(string userId)
        {
            var dbResult = await usersRepository.GetUserById(userId);
            if (dbResult.IsSucceeded)
            {
                return View(UserViewModel.Create(dbResult.Result));
            }
            return null;
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

        public async Task<ActionResult> OrderList()
        {
            var dbResult = await orderesRepository.GetAllOrderAsync();
            if (dbResult.IsSucceeded)
            {
                return View(dbResult.Result.Select(OrderInfoModel.Create));
            }
            return null;
        }

        protected override void Dispose(bool disposing)
        {
            productRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}