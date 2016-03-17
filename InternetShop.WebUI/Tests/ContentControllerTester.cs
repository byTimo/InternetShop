using System.Collections.Generic;
using System.Linq;
using InternetShop.DataLayer;
using InternetShop.DataLayer.Abstract;
using InternetShop.DataLayer.Entities;
using InternetShop.WebUI.Controllers;
using InternetShop.WebUI.Models.ProductModels;
using Moq;
using NUnit.Framework;

namespace InternetShop.WebUI.Tests
{
    [TestFixture]
    public class ContentControllerTester
    {
        private ContentController controller;
        private Mock<IProductsRepository> mock;
        private IEnumerable<Product> testData;
        private Cart cart;

        [SetUp]
        public void SetupTest()
        {
            var pageSize = 15;
            cart = new Cart();
            testData = Enumerable.Repeat(new Audio {ProductId = 1}, pageSize + 5).ToList();
            mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(testData);
            controller = new ContentController(mock.Object)
            {
                PageSize = pageSize
            };
        }

        [Test]
        public void ListFirstPageTest()
        {

            var pageModel = (ProductListViewModel) controller.List(cart).Model;

            Assert.That(pageModel.Products.Count(), Is.EqualTo(controller.PageSize));
            CollectionAssert.AreEqual(testData.Take(controller.PageSize), pageModel.Products.Select(p => p.ToProduct()));
            Assert.That(pageModel.PagingInfo.CurrentPage, Is.EqualTo(1));
        }

        [Test]
        public void ListSecondPageTest()
        {
            var pageModel = (ProductListViewModel) controller.List(cart, 2).Model;

            Assert.That(pageModel.Products.Count(), Is.EqualTo(5));
            CollectionAssert.AreEqual(testData.Skip(controller.PageSize).ToList(), pageModel.Products.Select(p => p.ToProduct()));
            Assert.That(pageModel.PagingInfo.CurrentPage, Is.EqualTo(2));
        }
    }
}