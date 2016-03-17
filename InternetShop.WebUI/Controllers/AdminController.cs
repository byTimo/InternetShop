using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models.ProductModels;

namespace InternetShop.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private  readonly IProductsRepository repository;

        public AdminController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult ProductList()
        {
            var allProducts = new ProductListViewModel
            {
                Products = repository.Products.Select(ProductViewModel.Create)
            };
            return View(allProducts);
        }

        public ViewResult CreateProduct()
        {
            ViewBag.Title = "Админ панель : Создание товара";
            return View(new ProductViewModel());
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(productViewModel.ToProduct());
                TempData["message"] = $"Добавлен новый товар: {productViewModel.Name}";
                return RedirectToAction("ProductList");
            }
            TempData["error-message"] = "При создании товара возникли ошибки";
            return View(productViewModel);
        }

        public ViewResult EditProduct(int productId)
        {
            ViewBag.Title = "Админ панель : изменение товара";
            var product = repository.Products.First(p => p.ProductId == productId);
            var productViewModel = ProductViewModel.Create(product);
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = productViewModel.ToProduct();
                repository.SaveProduct(product);
                TempData["message"] = $"Изменения товара {product.Name} были сохранены!";
                return RedirectToAction("ProductList");
            }
            TempData["error-message"] = "При обновлении товара возникли ошибки!";
            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult DeleteProduct(int productId)
        {
            var product = repository.Products.First(p => p.ProductId == productId);
            repository.DeleteProduct(product);
            TempData["message"] = $"Товар {product.Name} успешно удалён!";
            return RedirectToAction("ProductList");
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}