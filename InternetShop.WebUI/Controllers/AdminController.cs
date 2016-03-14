using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;

namespace InternetShop.WebUI.Controllers
{
    public class AdminController : Controller
    {
        private  readonly IProductsRepository repository;

        public AdminController(IProductsRepository repository)
        {
            this.repository = repository;
        }

        public ViewResult List()
        {
            var allProducts = repository.Products;
            return View(allProducts);
        }
    }
}