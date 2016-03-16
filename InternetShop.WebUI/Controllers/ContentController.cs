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

        public ViewResult List(Cart cart, int page = 1)
        {
            var model = new ProductListViewModel
            {
                Products = productsRepository.Products
                    .Skip((page - 1)*PageSize)
                    .Take(PageSize)
                    .Select(ProductViewModel.Create),
                PagingInfo = new PagingInfo(page, PageSize, productsRepository.Products.Count())
            };
            ViewBag.ProductInCartCount = cart.ProductsInCart.Count;
            return View(model);
        }

        public FileContentResult GetImage(int productId)
        {
            var product = productsRepository.Products.First(p => p.ProductId == productId);
            return File(product.ImageData, product.ImageMimeType);
        }


        public ActionResult AddToCart(Cart cart, int productId)
        {
            var product = productsRepository.Products.First(p => p.ProductId == productId);
            cart.AddItem(product, 1);
            TempData["Message"] = $"Товар {product.Name} добавлен в корзину.";
            return RedirectToAction("List");
        }

        public ActionResult RemoveFromCart(Cart cart, int productId)
        {
            var product = productsRepository.Products.First(p => p.ProductId == productId);
            cart.RemoveItem(product);
            TempData["Message"] = $"Товар {product.Name} удалён из корзины.";
            return RedirectToAction("Cart");
        }

        public ActionResult ClearCart(Cart cart)
        {
            cart.Clear();
            TempData["Message"] = "Корзина отчищена";
            return RedirectToAction("Cart");
        }

        public ViewResult Cart(Cart cart)
        {
            return View(cart);
        }
    }
}