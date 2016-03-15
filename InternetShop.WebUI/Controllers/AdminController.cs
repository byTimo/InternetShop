using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models;

namespace InternetShop.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private  readonly IProductsRepository repository;

        public AdminController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult ProductList()
        {
            var allProducts = repository.Products;
            return View(allProducts);
        }

        public PartialViewResult EditProduct(int productId)
        {
            var product = repository.Products.First(p => p.ProductId == productId);
            var productViewModel = new ProductViewModel(product);
            return PartialView(productViewModel);
        }

        [HttpPost]
        public ActionResult EditProduct(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = productViewModel.ToProduct();
                repository.SaveProduct(product);
                return RedirectToAction("ProductList");
            }
            return PartialView(productViewModel);
        }

        protected override void Dispose(bool disposing)
        {
            repository.Dispose();
            base.Dispose(disposing);
        }
    }
}