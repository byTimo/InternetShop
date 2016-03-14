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
        private AdminController controller;
        private Mock<IProductsRepository> mock;
        private IEnumerable<Product> testData;

        [SetUp]
        public void SetupTest()
        {
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
    }
}