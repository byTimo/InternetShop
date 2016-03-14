using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.WebUI.Models;

namespace InternetShop.WebUI.Controllers
{
    public class ContentController : Controller
    {
        public int PageSize { get; set; } = 9;

        private readonly IProductsRepository productsRepository;

        public ContentController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public ViewResult List(int page = 1)
        {
            var model = new ProductViewModel
            {
                Products = productsRepository.Products
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo(page, PageSize, productsRepository.Products.Count())
            };

            return View(model);
        }
    }
}