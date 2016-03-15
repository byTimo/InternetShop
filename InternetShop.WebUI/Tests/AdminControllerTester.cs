using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Controllers;
using InternetShop.WebUI.Models;
using Moq;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class AdminControllerTester
    {
        private AdminController controller;
        private Mock<IProductsRepository> mock;
        private IEnumerable<Product> testData;
        private Audio audioTestProduct;

        [SetUp]
        public void SetupTest()
        {
            audioTestProduct = new Audio
            {
                ProductId = 4,
                Name = "Audio product",
                Description = "This is audio product",
                Year = 2016,
                Price = 30000,
                MusicalDirection = "Hip-hop",
                Perfomer = "byTimo"
            };
            testData = Enumerable.Repeat(new Audio(), 20);
            mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(testData);
            controller = new AdminController(mock.Object);
        }

        [Test]
        public void GetAllProductsTest()
        {
            var result = controller.ProductList();
            var productsInPage = (IEnumerable<Product>) result.Model;

            CollectionAssert.AreEqual(productsInPage, testData);
        }

        [Test]
        public void SaveNewProductTest()
        {
            audioTestProduct.ProductId = 0;
            var productViewModel = new ProductViewModel(audioTestProduct);
            var resutl = controller.EditProduct(productViewModel);

            mock.Verify(m => m.SaveProduct(productViewModel.Product));
            Assert.IsInstanceOf<RedirectToRouteResult>(resutl);
        }

        [Test]
        public void SaveValidChangesTest()
        {
            var testProduct = testData.ToArray()[5];
            testProduct.Name = "Test";
            testProduct.Price = 300;

            var result = controller.EditProduct(new ProductViewModel(testProduct));

            mock.Verify(m => m.SaveProduct(testProduct));
            Assert.IsInstanceOf<RedirectToRouteResult>(result);
        }

        [Test]
        public void SaveInvalidChangesTest()
        {
            var product = new Audio();

            controller.ModelState.AddModelError("error", "error");
            var result = controller.EditProduct(new ProductViewModel(product));

            Assert.IsInstanceOf<PartialViewResult>(result);
        }
    }
}