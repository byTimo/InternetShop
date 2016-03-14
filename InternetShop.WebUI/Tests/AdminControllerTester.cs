using System.Collections.Generic;
using System.Linq;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Controllers;
using Moq;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class AdminControllerTester
    {
        [Test]
        public void GetAllProductsTest()
        {
            var products = Enumerable.Repeat(new Audio(), 20);
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(products);
            var controller = new AdminController(mock.Object);

            var result = controller.List();
            var productsInPage = (IEnumerable<Product>) result.Model;

            CollectionAssert.AreEqual(productsInPage, products);
        }
    }
}