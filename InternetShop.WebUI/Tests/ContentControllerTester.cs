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
    public class ContentControllerTester
    {
        [Test]
        public void ListFirstPageTest()
        {
            var testData = Enumerable.Repeat(new Audio {ProductId = 1}, 14).ToList();
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(testData);
            var controller = new ContentController(mock.Object);
            var pageSize = controller.PageSize;

            var pageModel = (ProductViewModel)controller.List().Model;

            Assert.That(pageModel.Products.Count(), Is.EqualTo(pageSize));
            CollectionAssert.AreEqual(testData.Take(pageSize), pageModel.Products);
            Assert.That(pageModel.PagingInfo.CurrentPage, Is.EqualTo(1));
        }

        [Test]
        public void ListSecondPageTest()
        {
            var testData = Enumerable.Repeat(new Audio(), 14).ToList();
            var mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(testData);
            var controller = new ContentController(mock.Object);
            var pageSize = controller.PageSize;

            var pageModel = ((ProductViewModel) controller.List(2).Model);

            Assert.That(pageModel.Products.Count(), Is.EqualTo(5));
            CollectionAssert.AreEqual(testData.Skip(pageSize).ToList(), pageModel.Products);
            Assert.That(pageModel.PagingInfo.CurrentPage, Is.EqualTo(2));
        }
    }
}