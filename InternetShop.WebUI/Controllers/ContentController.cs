using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Models;
using InternetShop.WebUI.Models.ProductModels;

namespace InternetShop.WebUI.Controllers
{
    public class ContentController : Controller
    {
        public int PageSize { get; set; } = 15;

        private readonly IProductsRepository productsRepository;

        public ContentController(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public async Task<ViewResult> List(Cart cart, User user, int page = 1)
        {
            var model = new ProductListViewModel
            {
                IdentityUserInfo = IdentityUserInfo.Create(user)
            };
            var dbResult = await productsRepository.GetAllProductAsync();
            if (dbResult.IsSucceeded)
            {
                model.Products = dbResult.Result.Skip((page - 1)*PageSize)
                    .Take(PageSize)
                    .Select(ProductViewModel.Create);
                model.PagingInfo = new PagingInfo(page, PageSize, dbResult.Result.Count());
            }
            ViewBag.ProductInCartCount = cart.ProductsInCart.Count;
            return View(model);
        }

        public async Task<FileContentResult> GetImage(int productId)
        {
            var dbResult = await productsRepository.GetProductById(productId);
            if (dbResult.IsSucceeded)
            {
                return File(dbResult.Result.ImageData, dbResult.Result.ImageMimeType);
            }
            return null;
        }

        public Task<ViewResult> Cart(Cart cart)
        {
            return Task.FromResult(View(cart));
        }

        public async Task<ActionResult> AddToCart(Cart cart, int productId)
        {
            var dbResult = await productsRepository.GetProductById(productId);
            if (dbResult.IsSucceeded)
            {
                cart.AddItem(dbResult.Result, 1);
                TempData["Message"] = $"Товар {dbResult.Result.Name} добавлен в корзину.";
            }
            return RedirectToAction("List");
        }

        public async Task<ActionResult> RemoveFromCart(Cart cart, int productId)
        {
            var dbResult = await productsRepository.GetProductById(productId);
            if (dbResult.IsSucceeded)
            {
                cart.RemoveItem(dbResult.Result);
                TempData["Message"] = $"Товар {dbResult.Result.Name} удалён из корзины.";
            }
            return RedirectToAction("Cart");
        }

        public Task<RedirectToRouteResult> ClearCart(Cart cart)
        {
            cart.Clear();
            TempData["Message"] = "Корзина очищена";
            return Task.FromResult(RedirectToAction("Cart"));
        }

        public async Task<PartialViewResult> Info(int productId)
        {
            var dbResult = await productsRepository.GetProductById(productId);
            if (dbResult.IsSucceeded)
            {
                var productViewModel = ProductViewModel.Create(dbResult.Result);
                return PartialView(productViewModel);
            }
            return null;
        }
    }
}