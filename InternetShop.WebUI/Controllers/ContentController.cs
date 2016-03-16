using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer;
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
            var model = new ProductListViewModel
            {
                Products = productsRepository.Products
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize)
                    .Select(ProductViewModel.Create),
                PagingInfo = new PagingInfo(page, PageSize, productsRepository.Products.Count())
            };
            ViewBag.ProductInCartCount = GetCart().ProductsInCart.Count;
            return View(model);
        }

        public ActionResult AddToCart(int productId)
        {
            var product = productsRepository.Products.First(p => p.ProductId == productId);
            GetCart().AddItem(product, 1);
            return RedirectToAction("List");
        }

        public ViewResult Cart()
        {
            var cart = GetCart();
            return View(cart);
        }

        private Cart GetCart()
        {
            var cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
    }
}